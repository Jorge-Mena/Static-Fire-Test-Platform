/**************************************************************
 * ESP32 Based motor static fire test platform
 * 
 * Features:
 * - HX711 Load Cell measurement
 * - MAX6675 Thermocouple temperature reading (dual sensors)
 * - SD Card data logging
 * - WiFi connectivity with TCP server
 * - Comprehensive self-test routines
 * - Visual and audible status indicators
 * 
 * Hardware Connections:
 * - HX711: DOUT=13, CLK=14
 * - MAX6675: DO=35, CS=32, CLK=33 (thermocouple1)
 * - MAX6675: DO=25, CS=26, CLK=27 (thermocouple2)
 * - SD Card: MISO=19, CLK=18, CS=5, MOSI=23
 * - LEDs: Red=15, Blue=2
 * - Buzzer=4, Button=34
 **************************************************************/

#include <time.h>
#include <WiFi.h>
#include <Preferences.h>
#include <SPI.h>
#include <SD.h>
#include "HX711.h"
#include "max6675.h"

// ====================== CONSTANTS & CONFIGURATION ======================
// WiFi credential commands
const char* PORT_CMD = "PORT";
const char* SSID_CMD = "SSID";
const char* PASSWORD_CMD = "PWD";

// Hardware pin configuration
const int LOAD_CELL_DOUT_PIN = 13;    // HX711 Data pin
const int LOAD_CELL_CLK_PIN = 14;     // HX711 Clock pin

const int THERMO1_DO_PIN = 35;        // MAX6675 1 Data pin
const int THERMO1_CS_PIN = 32;        // MAX6675 1 Chip Select
const int THERMO1_CLK_PIN = 33;       // MAX6675 1 Clock pin

const int THERMO2_DO_PIN = 25;        // MAX6675 2 Data pin
const int THERMO2_CS_PIN = 26;        // MAX6675 2 Chip Select
const int THERMO2_CLK_PIN = 27;       // MAX6675 2 Clock pin

const int SD_MISO_PIN = 19;           // SD Card MISO
const int SD_CLK_PIN = 18;            // SD Card CLK
const int SD_CS_PIN = 5;              // SD Card CS
const int SD_MOSI_PIN = 23;           // SD Card MOSI

const int LED_RED_PIN = 15;           // Red indicator LED
const int LED_BLUE_PIN = 2;           // Blue indicator LED
const int BUZZER_PIN = 4;             // Buzzer
const int BUTTON_REC_PIN = 34;        // Record button

// HX711 calibration values
const float CALIBRATION_FACTOR = 1717.9869f;
const float OFFSET = 5365.13867188f;

// File system configuration
const char* TEST_FILE_PATH = "/tempTest.txt";
const char* TEST_WRITE_TEXT = "WRITE_TEST_123";
int numFile = 1;

// Timing constants
const unsigned long READ_INTERVAL = 250;       // Sensor read interval (ms)
const unsigned long SD_WRITE_INTERVAL = 1000;  // SD card write interval (ms)
const unsigned long BUTTON_DEBOUNCE = 100;     // Button debounce interval (ms)

// Message prefixes
const String MSG_SYSTEM = "[SYSTEM] ";
const String MSG_ERROR = "[ERROR] ";
const String MSG_WARNING = "[WARNING] ";
const String MSG_DATA = "";
const String MSG_TEST = "[TEST] ";
const String MSG_STATUS = "[STATUS] ";

// ====================== DATA STRUCTURES ======================
struct Credentials {
    int port;
    String ssid;
    String password;
};

struct SelfTest {
    bool serialPassed = false;
    bool tcpServerPassed = false;
    bool hx711Passed = false;
    bool max6675_1Passed = false;
    bool max6675_2Passed = false;
    bool sdCardPassed = false;
};

struct SensorData {
    float timeStamp;
    float temperature1;
    float temperature2;
    float mass;
};

// ====================== GLOBAL OBJECTS ======================
Preferences preferences;
HX711 scale;
MAX6675 thermocouple1(THERMO1_CLK_PIN, THERMO1_CS_PIN, THERMO1_DO_PIN);
MAX6675 thermocouple2(THERMO2_CLK_PIN, THERMO2_CS_PIN, THERMO2_DO_PIN);
WiFiClient client;
WiFiServer* server = nullptr;
String logFilePath;

// ====================== MESSAGE FUNCTIONS ======================
void writeMessage(const String& type, const String& message) {
    String fullMessage = type + message;
    
    if (client && client.connected()) {
        client.println(fullMessage);
    }
    if (Serial) {
        Serial.println(fullMessage);
    }
}

void writeData(const String& line, const String& logFilePath) {
    writeMessage(MSG_DATA, line);
    
    File logFile = SD.open(logFilePath, FILE_APPEND);
    if (logFile) {
        logFile.println(line);
        logFile.close();
    }
}

// ====================== SYSTEM CONTROL FUNCTIONS ======================
void shutdownESP32() {
    writeMessage(MSG_SYSTEM, "Shutting down system...");
    digitalWrite(LED_RED_PIN, LOW);
    digitalWrite(LED_BLUE_PIN, LOW);
    
    tone(BUZZER_PIN, 800, 100);
    delay(150);
    tone(BUZZER_PIN, 600, 100);
    delay(150);
    tone(BUZZER_PIN, 400, 200);
    
    if (SD.begin(SD_CS_PIN)) SD.end();
    if (client && client.connected()) client.stop();
    if (server) server->stop();
    
    delay(1000);
    esp_deep_sleep_start();
}

void blinkIndicatorLED(int pin, int times = 3, int interval = 200) {
    for (int i = 0; i < times; i++) {
        digitalWrite(pin, HIGH);
        delay(interval);
        digitalWrite(pin, LOW);
        if (i < times - 1) delay(interval);
    }
}

// ====================== AUDIO FEEDBACK FUNCTIONS ======================
void bootSignal() {
    tone(BUZZER_PIN, 1000, 100);
    delay(120);
    tone(BUZZER_PIN, 1500, 100);
    delay(120);
    tone(BUZZER_PIN, 2000, 100);
    delay(150);
    blinkIndicatorLED(LED_RED_PIN, 5, 50);
    blinkIndicatorLED(LED_BLUE_PIN, 5, 50);
}

void selfTestBegin() {
    tone(BUZZER_PIN, 600, 80);  
    delay(100);
    tone(BUZZER_PIN, 1000, 80);
    delay(100);
    tone(BUZZER_PIN, 1400, 80);
    delay(100);
    tone(BUZZER_PIN, 1800, 80);
    delay(150);
    blinkIndicatorLED(LED_BLUE_PIN, 4, 25);
}

void selfTestPass() {
    tone(BUZZER_PIN, 1000, 80);
    delay(100);
    tone(BUZZER_PIN, 1500, 80);
    delay(100);
    tone(BUZZER_PIN, 2000, 120); 
    delay(150);
    blinkIndicatorLED(LED_BLUE_PIN, 3, 25);
}

void selfTestFail() {
    tone(BUZZER_PIN, 800, 100);  
    delay(120);
    tone(BUZZER_PIN, 600, 100);  
    delay(120);
    tone(BUZZER_PIN, 400, 250);  
    delay(300);
    blinkIndicatorLED(LED_BLUE_PIN, 4, 50);
}

void recordingStartSound() {
    tone(BUZZER_PIN, 880, 100);
    delay(50);
    tone(BUZZER_PIN, 1175, 200);
}

void recordingStopSound() {
    tone(BUZZER_PIN, 1175, 100);
    delay(50);
    tone(BUZZER_PIN, 880, 200);
}

void errorSound() {
    tone(BUZZER_PIN, 300, 100);
    delay(50);
    tone(BUZZER_PIN, 200, 200);
    delay(50);
    tone(BUZZER_PIN, 150, 300);
}

// ====================== WIFI & STORAGE FUNCTIONS ======================
void saveCredentials(const Credentials& creds) {
    preferences.begin("wifi", false);
    preferences.putString("ssid", creds.ssid);
    preferences.putString("password", creds.password);
    preferences.putInt("port", creds.port);
    preferences.end();
}

Credentials loadCredentials() {
    Credentials creds;
    preferences.begin("wifi", true);
    creds.ssid = preferences.getString("ssid", "");
    creds.password = preferences.getString("password", "");
    creds.port = preferences.getInt("port", 23);
    preferences.end();
    return creds;
}

String generateUniqueFilename(const String &baseName, const String &extension = ".csv") {
    char buffer[32];
    sprintf(buffer, "/%s_%03d%s", baseName.c_str(), numFile, extension.c_str());
    numFile++;
    return String(buffer);
}

Credentials getWifiCredentials(unsigned long timeoutMs) {
    Credentials creds;
    unsigned long start = millis();

    writeMessage(MSG_SYSTEM, "Getting WiFi credentials...");
    writeMessage(MSG_SYSTEM, String(PORT_CMD));

    while (creds.port == 0 && (millis() - start) < timeoutMs) {
        if (Serial.available()) {
            String input = Serial.readStringUntil('\n');
            input.trim();
            creds.port = input.toInt();
            if (creds.port != 0) break;
        }
        delay(1000);
    }

    writeMessage(MSG_SYSTEM, String(SSID_CMD));
    while (creds.ssid.length() == 0 && (millis() - start) < timeoutMs) {
        if (Serial.available()) {
            creds.ssid = Serial.readStringUntil('\n');
            creds.ssid.trim();
            if (creds.ssid.length() > 0) break;
        }
        delay(1000);
    }

    writeMessage(MSG_SYSTEM, String(PASSWORD_CMD));
    while (creds.password.length() == 0 && (millis() - start) < timeoutMs) {
        if (Serial.available()) {
            creds.password = Serial.readStringUntil('\n');
            creds.password.trim();
            if (creds.password.length() > 0) break;
        }
        delay(1000);
    }

    if (creds.port > 0 && creds.ssid.length() > 0 && creds.password.length() > 0) {
        saveCredentials(creds);
        writeMessage(MSG_SYSTEM, "Credentials saved");
    } else {
        tone(BUZZER_PIN, 1000, 60);
        delay(10);
        tone(BUZZER_PIN, 1000, 60);
        writeMessage(MSG_WARNING, "Using stored credentials");
        creds = loadCredentials();
    }
    return creds;
}

bool initializeServer(const Credentials& creds) {
    WiFi.begin(creds.ssid.c_str(), creds.password.c_str());

    writeMessage(MSG_SYSTEM, "Connecting to WiFi...");
    unsigned long start = millis();
    while (WiFi.status() != WL_CONNECTED && (millis() - start) < 30000) {
        delay(500);
        writeMessage(MSG_SYSTEM, ".");
    }

    if (WiFi.status() != WL_CONNECTED) {
        errorSound();
        writeMessage(MSG_ERROR, "WiFi connection failed");
        return false;
    }

    tone(BUZZER_PIN, 800, 50);
    writeMessage(MSG_SYSTEM, "Connected to: " + creds.ssid);
    writeMessage(MSG_SYSTEM, "IP " + WiFi.localIP().toString());
    writeMessage(MSG_SYSTEM, "Signal strength: " + String(WiFi.RSSI()) + " dBm");

    if (server == nullptr) {
        server = new WiFiServer(creds.port);
    }
    server->begin();
    server->setNoDelay(true);
    writeMessage(MSG_SYSTEM, "TCP server started on port " + String(creds.port));
    return true;
}

bool initializeSDCard() {
    SPI.begin(SD_CLK_PIN, SD_MISO_PIN, SD_MOSI_PIN, SD_CS_PIN);
    
    if (!SD.begin(SD_CS_PIN)) {
        errorSound();
        writeMessage(MSG_ERROR, "SD card initialization failed");
        return false;
    }
    
    uint8_t cardType = SD.cardType();
    if (cardType == CARD_NONE) {
        errorSound();
        writeMessage(MSG_ERROR, "No SD card detected");
        return false;
    }
    
    writeMessage(MSG_SYSTEM, "SD card ready (" + 
                String(SD.totalBytes() / (1024.0 * 1024.0)) + " MB total, " +
                String(SD.usedBytes() / (1024.0 * 1024.0)) + " MB used)");
    return true;
}

// ====================== SELF-TEST FUNCTIONS ======================
bool serialSelfTest() {
    writeMessage(MSG_TEST, "Serial communication test - please reply 'OK'");
    while (Serial.available()) Serial.read();

    String receivedData;
    unsigned long start = millis();
    while (millis() - start < 30000) {
        if (Serial.available()) {
            receivedData = Serial.readStringUntil('\n');
            receivedData.trim();
            break;
        }
        delay(10);
    }

    if (receivedData == "OK") {
        writeMessage(MSG_TEST, "Serial test PASSED");
        return true;
    }
    
    writeMessage(MSG_ERROR, "Serial test FAILED - " + 
                (receivedData.length() > 0 ? 
                 "Unexpected response: " + receivedData : "No response"));
    return false;
}

bool serverSelfTest(WiFiClient &client) {
    writeMessage(MSG_TEST, "TCP server test - please reply 'OK' from client");

    if (!client || !client.connected()) {
        writeMessage(MSG_ERROR, "TCP test FAILED - No client connected");
        return false;
    }

    client.println("[TEST] Please reply with 'OK'");

    String tcpResponse;
    unsigned long start = millis();
    while (millis() - start < 30000) {
        if (client.available()) {
            tcpResponse = client.readStringUntil('\n');
            tcpResponse.trim();
            break;
        }
        delay(10);
    }

    if (tcpResponse == "OK") {
        writeMessage(MSG_TEST, "TCP test PASSED");
        return true;
    }
    
    writeMessage(MSG_ERROR, "TCP test FAILED - " + 
                (tcpResponse.length() > 0 ? 
                 "Unexpected response: " + tcpResponse : "No response"));
    return false;
}

bool hx711SelfTest(HX711 &scale) {
    writeMessage(MSG_TEST, "Load cell test - ensure no load on sensor");

    scale.set_scale(CALIBRATION_FACTOR);
    scale.set_offset(OFFSET);
    
    long reading = scale.read_average(5);
    writeMessage(MSG_TEST, "Raw reading: " + String(reading));

    if (reading > 8000 || reading < 1000) {
        writeMessage(MSG_ERROR, "Load cell test FAILED - Unexpected value");
        return false;
    }
    
    writeMessage(MSG_TEST, "Load cell test PASSED");
    return true;
}

bool max6675SelfTest(MAX6675 &thermocouple, const String &name) {
    writeMessage(MSG_TEST, "Thermocouple " + name + " test - checking readings");

    float tempC = thermocouple.readCelsius();
    writeMessage(MSG_TEST, "Temperature " + name + ": " + String(tempC) + " °C");

    if (isnan(tempC)) {
        writeMessage(MSG_ERROR, "Thermocouple " + name + " test FAILED - Invalid reading");
        return false;
    } else if (tempC == 0.0) {
        writeMessage(MSG_ERROR, "Thermocouple " + name + " test FAILED - Possible open circuit");
        return false;
    }
    
    writeMessage(MSG_TEST, "Thermocouple " + name + " test PASSED");
    return true;
}

bool sdCardSelfTest() {
    writeMessage(MSG_TEST, "SD card test - verifying read/write");

    if (!initializeSDCard()) {
        writeMessage(MSG_ERROR, "SD card test FAILED - Initialization error");
        return false;
    }

    writeMessage(MSG_TEST, "Creating test file...");
    File testFile = SD.open(TEST_FILE_PATH, FILE_WRITE);
    if (!testFile) {
        writeMessage(MSG_ERROR, "SD card test FAILED - File creation");
        return false;
    }

    size_t bytesWritten = testFile.print(TEST_WRITE_TEXT);
    testFile.close();

    if (bytesWritten != strlen(TEST_WRITE_TEXT)) {
        writeMessage(MSG_ERROR, "SD card test FAILED - Write incomplete");
        SD.remove(TEST_FILE_PATH);
        return false;
    }

    testFile = SD.open(TEST_FILE_PATH);
    if (!testFile) {
        writeMessage(MSG_ERROR, "SD card test FAILED - File verification");
        SD.remove(TEST_FILE_PATH);
        return false;
    }

    String content = testFile.readString();
    testFile.close();

    if (content != TEST_WRITE_TEXT) {
        writeMessage(MSG_ERROR, "SD card test FAILED - Content mismatch");
        SD.remove(TEST_FILE_PATH);
        return false;
    }

    if (!SD.remove(TEST_FILE_PATH)) {
        writeMessage(MSG_WARNING, "SD card test note - Failed to delete test file");
    }

    writeMessage(MSG_TEST, "SD card test PASSED");
    return true;
}

String testStatus(bool passed) {
    return passed ? "PASS" : "FAIL";
}

SelfTest runSelfTest(const String &testType) {
    SelfTest testResult;
    String lowerType = testType;
    lowerType.toLowerCase();

    if (lowerType == "diagnostic -full") {
        selfTestBegin();
        blinkIndicatorLED(LED_BLUE_PIN, 2, 250);

        testResult.serialPassed = serialSelfTest();
        testResult.tcpServerPassed = serverSelfTest(client);
        testResult.hx711Passed = hx711SelfTest(scale);
        testResult.max6675_1Passed = max6675SelfTest(thermocouple1, "1");
        testResult.max6675_2Passed = max6675SelfTest(thermocouple2, "2");
        testResult.sdCardPassed = sdCardSelfTest();

        writeMessage(MSG_TEST, "\n=== Test Results ===");
        writeMessage(MSG_TEST, "Serial: " + testStatus(testResult.serialPassed));
        writeMessage(MSG_TEST, "TCP Server: " + testStatus(testResult.tcpServerPassed));
        writeMessage(MSG_TEST, "Load Cell: " + testStatus(testResult.hx711Passed));
        writeMessage(MSG_TEST, "Thermocouple 1: " + testStatus(testResult.max6675_1Passed));
        writeMessage(MSG_TEST, "Thermocouple 2: " + testStatus(testResult.max6675_2Passed));
        writeMessage(MSG_TEST, "SD Card: " + testStatus(testResult.sdCardPassed));

        if (testResult.serialPassed && testResult.tcpServerPassed && 
            testResult.hx711Passed && testResult.max6675_1Passed && 
            testResult.max6675_2Passed && testResult.sdCardPassed) {
            selfTestPass();
            writeMessage(MSG_SYSTEM, "All tests PASSED");
            digitalWrite(LED_BLUE_PIN, HIGH);
        } else {
            selfTestFail();
            writeMessage(MSG_ERROR, "Some tests FAILED");
        }
    }
    else if (lowerType == "diagnostic -fast") {
        selfTestBegin();
        blinkIndicatorLED(LED_BLUE_PIN, 3, 250);

        testResult.hx711Passed = hx711SelfTest(scale);
        testResult.max6675_1Passed = max6675SelfTest(thermocouple1, "1");
        testResult.max6675_2Passed = max6675SelfTest(thermocouple2, "2");
        testResult.sdCardPassed = sdCardSelfTest();

        writeMessage(MSG_TEST, "\n=== Test Results ===");
        writeMessage(MSG_TEST, "Load Cell: " + testStatus(testResult.hx711Passed));
        writeMessage(MSG_TEST, "Thermocouple 1: " + testStatus(testResult.max6675_1Passed));
        writeMessage(MSG_TEST, "Thermocouple 2: " + testStatus(testResult.max6675_2Passed));
        writeMessage(MSG_TEST, "SD Card: " + testStatus(testResult.sdCardPassed));

        if (testResult.hx711Passed && testResult.max6675_1Passed && 
            testResult.max6675_2Passed && testResult.sdCardPassed) {
            selfTestPass();
            writeMessage(MSG_SYSTEM, "All tests PASSED");
            digitalWrite(LED_BLUE_PIN, HIGH);
        } else {
            selfTestFail();
            writeMessage(MSG_ERROR, "Some tests FAILED");
        }
    }
    else if (lowerType == "diagnostic -nserial") {
        selfTestBegin();
        blinkIndicatorLED(LED_BLUE_PIN, 4, 250);

        testResult.tcpServerPassed = serverSelfTest(client);
        testResult.hx711Passed = hx711SelfTest(scale);
        testResult.max6675_1Passed = max6675SelfTest(thermocouple1, "1");
        testResult.max6675_2Passed = max6675SelfTest(thermocouple2, "2");
        testResult.sdCardPassed = sdCardSelfTest();

        writeMessage(MSG_TEST, "\n=== Test Results ===");
        writeMessage(MSG_TEST, "TCP Server: " + testStatus(testResult.tcpServerPassed));
        writeMessage(MSG_TEST, "Load Cell: " + testStatus(testResult.hx711Passed));
        writeMessage(MSG_TEST, "Thermocouple 1: " + testStatus(testResult.max6675_1Passed));
        writeMessage(MSG_TEST, "Thermocouple 2: " + testStatus(testResult.max6675_2Passed));
        writeMessage(MSG_TEST, "SD Card: " + testStatus(testResult.sdCardPassed));

        if (testResult.tcpServerPassed && testResult.hx711Passed && 
            testResult.max6675_1Passed && testResult.max6675_2Passed && 
            testResult.sdCardPassed) {
            selfTestPass();
            writeMessage(MSG_SYSTEM, "All tests PASSED");
            digitalWrite(LED_BLUE_PIN, HIGH);
        } else {
            selfTestFail();
            writeMessage(MSG_ERROR, "Some tests FAILED");
        }
    }
    else {
        writeMessage(MSG_ERROR, "Invalid test type specified");
        writeMessage(MSG_SYSTEM, "Available options:");
        writeMessage(MSG_SYSTEM, "- Diagnostic -full");
        writeMessage(MSG_SYSTEM, "- Diagnostic -fast");
        writeMessage(MSG_SYSTEM, "- Diagnostic -nserial");
    }
    return testResult;
}

// ====================== DATA RECORDING FUNCTIONS ======================
void recordData(unsigned long timestamp, File &logFile, String &dataBuffer, bool sdReady) {
    float mass = (scale.read() - OFFSET) / CALIBRATION_FACTOR;
    float temp1 = thermocouple1.readCelsius();
    float temp2 = thermocouple2.readCelsius();
    
    String dataLine = String(timestamp) + ";" + 
                     String(temp1, 2) + ";" + 
                     String(temp2, 2) + ";" + 
                     String(mass, 2);
    
    writeData(dataLine, logFilePath);
    
    if (sdReady) {
        dataBuffer += dataLine + "\n";
    }
}

void startRecording(bool &isRecording, bool &sdReady, unsigned long &recordingStartTime, unsigned long &lastReadTime, unsigned long &lastSDWriteTime, File &logFile, String &dataBuffer, String &logFilePath) {
    if (!isRecording) {
        recordingStartSound();
        writeMessage(MSG_SYSTEM, "Starting recording...");
        
        isRecording = true;
        recordingStartTime = millis();
        lastReadTime = 0;
        lastSDWriteTime = 0;
        dataBuffer = "";
        
        sdReady = initializeSDCard();
        if (sdReady) {
            logFilePath = generateUniqueFilename("Static_Fire_Log");
            logFile = SD.open(logFilePath, FILE_WRITE);
            
            if (logFile) {
                logFile.println("Timestamp(ms);Temperature1(C);Temperature2(C);Mass(Kg)");
                writeMessage(MSG_SYSTEM, "Log file created: " + logFilePath);
            } else {
                errorSound();
                writeMessage(MSG_ERROR, "Failed to create log file");
                sdReady = false;
            }
        } else {
            errorSound();
            writeMessage(MSG_WARNING, "SD card unavailable - data not saved");
        }
        
        digitalWrite(LED_RED_PIN, HIGH);
        writeMessage(MSG_SYSTEM, "Recording STARTED");
    }
}

void stopRecording(bool &isRecording, bool &sdReady, File &logFile, String &dataBuffer) {
    if (isRecording) {
        recordingStopSound();
        
        if (sdReady && dataBuffer.length() > 0 && logFile) {
            logFile.print(dataBuffer);
            dataBuffer = "";
        }
        
        if (sdReady && logFile) {
            logFile.close();
            writeMessage(MSG_SYSTEM, "Log file closed");
        }
        
        isRecording = false;
        sdReady = false;
        digitalWrite(LED_RED_PIN, LOW);
        writeMessage(MSG_SYSTEM, "Recording STOPPED");
    }
}

// ====================== COMMAND PROCESSING ======================
void processCommand(String message, bool &isRecording, bool &sdReady, unsigned long &recordingStartTime,
                   unsigned long &lastReadTime, unsigned long &lastSDWriteTime, 
                   File &logFile, String &dataBuffer, String &logFilePath) {
    message.trim();
    message.toLowerCase();

    if (isRecording && message == "stop") {
        stopRecording(isRecording, sdReady, logFile, dataBuffer);
        return;
    }
    
    if (message == "shutdown") {
        shutdownESP32();
        return;
    }
    
    if (!isRecording) {
        if (message.startsWith("diagnostic")) {
            runSelfTest(message);
        }
        else if (message == "start") {
            startRecording(isRecording, sdReady, recordingStartTime, lastReadTime, 
                         lastSDWriteTime, logFile, dataBuffer, logFilePath);
        }
        else if (message == "status") {
            tone(BUZZER_PIN, 800, 50);
            String statusMsg = "System status: ";
            statusMsg += isRecording ? "RECORDING" : "STANDBY";
            statusMsg += ", SD Card: ";
            statusMsg += sdReady ? "READY" : "NOT READY";
            statusMsg += ", WiFi: ";
            statusMsg += (WiFi.status() == WL_CONNECTED) ? "CONNECTED" : "DISCONNECTED";
            statusMsg += ", Clients: ";
            statusMsg += (client && client.connected()) ? "1" : "0";
            
            writeMessage(MSG_STATUS, statusMsg);
        }
    }
}

// ====================== MAIN SETUP & LOOP ======================
void setup() {
    pinMode(LED_BLUE_PIN, OUTPUT);
    pinMode(LED_RED_PIN, OUTPUT);
    pinMode(BUZZER_PIN, OUTPUT);
    pinMode(BUTTON_REC_PIN, INPUT);
    digitalWrite(LED_BLUE_PIN, LOW);
    digitalWrite(LED_RED_PIN, LOW);
    noTone(BUZZER_PIN);
    
    Serial.begin(115200);
    writeMessage(MSG_SYSTEM, "System initializing...");
    writeMessage(MSG_SYSTEM, "Running firmware version 4.1.1");

    while (digitalRead(BUTTON_REC_PIN) != HIGH) {
        delay(1);
    }

    bootSignal();

    scale.begin(LOAD_CELL_DOUT_PIN, LOAD_CELL_CLK_PIN);
    scale.set_scale(CALIBRATION_FACTOR);
    scale.set_offset(OFFSET);

    if (!initializeSDCard()) {
        errorSound();
        writeMessage(MSG_ERROR, "SD card initialization failed - restarting");
        delay(2000);
        ESP.restart();
    }

    SelfTest initialTest = runSelfTest("Diagnostic -fast");
    if (!initialTest.hx711Passed || !initialTest.max6675_1Passed || 
        !initialTest.max6675_2Passed || !initialTest.sdCardPassed) {
        errorSound();
        writeMessage(MSG_ERROR, "Hardware test failed - restarting");
        delay(1000);
        ESP.restart();
    }

    Credentials creds = getWifiCredentials(30000);
    if (!initializeServer(creds)) {
        errorSound();
        writeMessage(MSG_WARNING, "WiFi failed - running in offline mode");
    }

    writeMessage(MSG_SYSTEM, "System ready - Send commands or press button");
    tone(BUZZER_PIN, 2000, 200);
    delay(100);
    tone(BUZZER_PIN, 1500, 50);
    delay(25);
    tone(BUZZER_PIN, 1500, 50);
}

void loop() {
    static bool isRecording = false;
    static bool sdReady = false;
    static unsigned long recordingStartTime = 0;
    static unsigned long lastReadTime = 0;
    static unsigned long lastSDWriteTime = 0;
    static File logFile;
    static String dataBuffer;
    static String logFilePath;
    static unsigned long lastButtonCheck = 0;

    if (!client || !client.connected()) {
        client = server->available();
        if (client) {
            tone(BUZZER_PIN, 800, 50);
            writeMessage(MSG_SYSTEM, "Client connected");
        }
    }

    if (millis() - lastButtonCheck >= BUTTON_DEBOUNCE) {
        lastButtonCheck = millis();
        if (digitalRead(BUTTON_REC_PIN) == HIGH) {
            if (!isRecording) {
                startRecording(isRecording, sdReady, recordingStartTime, lastReadTime, 
                             lastSDWriteTime, logFile, dataBuffer, logFilePath);
            } else {
                stopRecording(isRecording, sdReady, logFile, dataBuffer);
            }
            while (digitalRead(BUTTON_REC_PIN) == HIGH) {
                delay(10);
            }
        }
    }

    if (Serial.available()) {
        String message = Serial.readStringUntil('\n');
        processCommand(message, isRecording, sdReady, recordingStartTime, 
                      lastReadTime, lastSDWriteTime, logFile, dataBuffer, logFilePath);
    }

    if (client && client.available()) {
        String message = client.readStringUntil('\n');
        processCommand(message, isRecording, sdReady, recordingStartTime, 
                      lastReadTime, lastSDWriteTime, logFile, dataBuffer, logFilePath);
    }

    if (isRecording) {
        unsigned long currentTime = millis();
        unsigned long elapsedTime = currentTime - recordingStartTime;
        
        if (currentTime - lastReadTime >= READ_INTERVAL) {
            recordData(elapsedTime, logFile, dataBuffer, sdReady);
            lastReadTime = currentTime;
        }
        
        if (sdReady && currentTime - lastSDWriteTime >= SD_WRITE_INTERVAL && dataBuffer.length() > 0) {
            logFile.print(dataBuffer);
            logFile.flush();
            dataBuffer = "";
            lastSDWriteTime = currentTime;
        }
    }

    delay(1);
}