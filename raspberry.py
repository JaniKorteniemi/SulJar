import serial
import time
import urllib.request, json
import requests

url = 'http://ec2-35-172-180-97.compute-1.amazonaws.com'
pclast = '/pc/last'
rbcheck = '/rb/check'
rbput = '/rb/put'
rb = '/rb'
playMax = '/lastsave'
saves = '/save'
first = '/firstsave'

# # # # # # # # # # # # # # # # # # # # 
port = serial.Serial("/dev/ttyS0",
        baudrate = 9600,
        timeout = 2.0,
        parity = serial.PARITY_NONE,
        stopbits = serial.STOPBITS_ONE,
        bytesize = serial.EIGHTBITS
        )
# # # # # # # # # # # # # # # # # # # #

deg = 0
speed = 0.0
maxSave = 0
currentSave = 0
play = 0

while True:
    #request_conn = requests.get(url + rbcheck)
    request = requests.get(url + pclast)
    
    #request_text1 = request_conn.text
    request_text = request.text
    
    #conn = json.loads(request_text1)
    #check = json.loads(request_text1)
    data = json.loads(request_text)
    #check = conn[0]['connectionCheck']
    
    repeatControl = data[0]['repeatControl']
    button = data[0]['button']
    
    if button == 3 and repeatControl == 1:
        play = 1
        lastSave = requests.get(url + playMax)
        lastSave_txt = lastSave.text
        lastSavetxt = json.loads(lastSave_txt)
        maxSave = lastSavetxt[0]['idrecordedInputs']
        
        firstSave = requests.get(url + first)
        firstSave_txt = firstSave.text
        firstSaveJson = json.loads(firstSave_txt)
        currentSave = firstSaveJson[0]['idrecordedInputs']
    
    while play == 1:
        print('play = 1')
        loadSave = requests.get(url + saves)
        loadSave_txt = loadSave.text
        loadSavejson = json.loads(loadSave_txt)
        
        if currentSave < maxSave:
            speedSave = loadSavejson[0]['speedControl']
            turnSave = loadSavejson[0]['turnControl']
            #currentSave = loadSavejson[0]['idrecordedInputs']
            play = 1
            
            #turn määrittely nucleolle
            if turnSave > 0:
                turnCtrl = '1000'
            elif turnSave < 0:
                turnCtrl = '2000'
            else:
                turnCtrl = '0'
                
            #speed määrittely nucleolle
            if speedSave > 0:
                speedCtrl = '1'
            elif speedSave < 0:
                speedCtrl = '2'
            else:
                speedCtrl = '0'
                
            resS = bytes(str(speedCtrl), 'utf-8')
            resT = bytes(str(turnCtrl), 'utf-8')
            port.write(resT + 'a'.encode('utf-8') + resS + 'b'.encode('utf-8'))   #nucleolle kirjoitus
            #print(resT + 'a'.encode('utf-8') + resS + 'b'.encode('utf-8')) #printtaa kirjoitus
            currentSave = currentSave + 1
            
            #if port.inWaiting() > 0:
                #bdeg = port.readline()       #nucleo kulma
                #bspeed = port.readline()     #nucleo nopeus
                #deg = 0
                #speed = 0
                #print (bdeg.decode('utf-8'))
                #print (bspeed.decode('utf-8'))
                #turnctrl = port.readline()
                #speedctrl = port.readline()
                #print(turnctrl.decode('utf-8'))
                #print("s " + speedctrl.decode('utf-8'))
                
                #gyron datan lähetys aws
                #nucData = {'Deg': deg, 'Speed': speed}
                #r = requests.post(url + rb, headers = {'content-type': 'application/json'}, json = json.dumps(nucData), verify = False)
                #r.encoding = 'utf-8'
            time.sleep(0.1)

        else:
            currentSave = 0
            maxSave = 0
            play = 0
            print('play = 0')

    if play == 0:
        #req = requests.put(url + rbput)
        #req_text = req.text
        idpcOut = data[0]['idpcOut']
        speedControl = data[0]['speedControl']
        turnControl = data[0]['turnControl']
        
        #turn määrittely nucleolle
        if turnControl > 0:
            turnCtrl = '1000'
        elif turnControl < 0:
            turnCtrl = '2000'
        else:
            turnCtrl = '0'
        
        #speed määrittely nucleolle    
        if speedControl > 0:
            speedCtrl = '1'
        elif speedControl < 0:
            speedCtrl = '2'
        else:
            speedCtrl = '0'

        print("ID:%s\nSPEED: %s\nTURN: %s\nBUTTON: %s\nREPEAT: %s"
               %(idpcOut, speedCtrl, turnCtrl, button, repeatControl))

        resS = bytes(str(speedCtrl), 'utf-8')
        resT = bytes(str(turnCtrl), 'utf-8')
        port.write(resT + 'a'.encode('utf-8') + resS + 'b'.encode('utf-8')) #nucleolle kirjoitus
        #print(resT + 'a'.encode('utf-8') + resS + 'b'.encode('utf-8'))
        if port.inWaiting() > 0:
            #deg = port.readline()       #nucleo kulma
            #speed = port.readline()     #nucleo nopeus
            #print (deg.decode('utf-8'))
            #print (speed.decode('utf-8'))
            
            turnctrl = port.readline()
            speedctrl = port.readline()
            print(turnctrl.decode('utf-8'))
            print(speedctrl.decode('utf-8'))
