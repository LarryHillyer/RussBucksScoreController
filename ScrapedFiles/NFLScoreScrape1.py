
import time
from time import strftime, gmtime, localtime, strptime, sleep
import datetime
from bs4 import BeautifulSoup
import requests
import dicttoxml
import xml.etree.ElementTree as ET
import shelve
import os.path
import sys

  
def scrape(url, i,errorFlag):

    gameList = []
    gametime = []
    awayTeams = []
    homeTeams = []
    teams=[]
    scores=[]
    clean={}
    gameTime = []
    scheduleTime = []
    scheduleDates = []
    gamecodes = []
    
    url='http://sports.yahoo.com/nfl/scoreboard'
    print url

    try:
        r=requests.get(url)
        soup=BeautifulSoup(r.content)
        gameSoup = soup.find_all('tr', {'class':'game pre link'})
        homeSoup = BeautifulSoup(str(gameSoup)).find_all('td',{'class':'home'})
        awaySoup = BeautifulSoup(str(gameSoup)).find_all('td',{'class':'away'})
        scoreSoup = BeautifulSoup(str(gameSoup)).find_all('td', {'class':'score'})
        summarySoup = BeautifulSoup(str(gameSoup)).find_all('td', {'class':'summary'})
        timeSoup = BeautifulSoup(str(gameSoup)).find_all('span', {'class':'time'})
        homeTeamSoup = BeautifulSoup(str(homeSoup)).find_all('span',{'class':'team'})
        awayTeamSoup = BeautifulSoup(str(awaySoup)).find_all('span',{'class':'team'})
        vsSoup = BeautifulSoup(str(scoreSoup)).find_all('h4', {'class':'vs'})
        

        for teams in homeTeamSoup:
            team = teams.em.text
            homeTeams.append(team)

        for teams in awayTeamSoup:
            team=teams.em.text
            awayTeams.append(team)
                
        for codes in vsSoup:

            try:
                gamecode = codes.a['href']
                gc1 =gamecode.split('/')
                gc2 = gc1[2].split('-')
                gamecode = gc2[len(gc2)-1]
                gamecodes.append(gamecode)
                date1=datetime.datetime.strptime(gamecode[0:8],'%Y%m%d')
                date2=date1.strftime('%m/%d/%Y')
                scheduleDates.append(date2)
            except:
                pass

        for times in timeSoup:
            time1 = times.text
            scheduleTime.append(time1)
                
    except:
        print 'Bad Dog'
        errorFlag='1'
        return scheduleDates,gamecodes,scheduleTime,gameTime,awayTeams,homeTeams,gameList,errorFlag                  
    return scheduleDates,gamecodes,scheduleTime,gameTime,awayTeams,homeTeams,gameList,errorFlag            

def dataScrub(scheduleTime,gameTime,awayTeams,homeTeams,gameList,errorFlag):
    errorFlag='0'
    
    for i1 in range(len(scheduleTime)):

        try:
            print scheduleTime[i1]
            sT1=scheduleTime[i1].split(' ')
            scheduleTime[i1]= sT1[0] + ' ' + sT1[1]
            print scheduleTime[i1]
            timeStamp = time.strptime(scheduleTime[i1], '%I:%M %p')
            timeStamp1 = time.strftime('%H%M',timeStamp)
            hour = int(timeStamp1[:2]) - 2
            mTime = str(hour) + timeStamp1[-2:]
            mTime = time.strptime(mTime, '%H%M')
            timeStamp1 = time.strftime('%I:%M %p', mTime)
            if timeStamp1[0] == '0':
                timeStamp1 = timeStamp1[1:]   
            gameTime.append(timeStamp1)
        except:
            gameTime.append('TBA')
            errorFlag='1'
        
    for i in range(len(scheduleTime)):
        num = 'game' + str(i + 1)
        gameList.append(num)

    return errorFlag
    
def makeSchedule(gameTime,homeTeams,awayTeams,scheduleTime,currentDate,currentTime,gamecodes,scheduleDates):  
    score = ET.Element('score')
    score.attrib['filedate'] = currentDate 
    score.attrib['filetime'] = currentTime
    print scheduleTime
    print str(len(scheduleTime))
    
    for i1 in range(len(scheduleTime)):
        game = ET.SubElement(score,'game')
        game.attrib['hometeam'] = homeTeams[i1]
        game.attrib['awayteam'] = awayTeams[i1]
        game.attrib['gamecode'] = gamecodes[i1] 
        homescore = ET.SubElement(game, 'homescore')
        homescore.text = '0'           
        awayscore = ET.SubElement(game, 'awayscore')
        awayscore.text = '0'            
        gametime = ET.SubElement(game, 'gametime')
        gametime.text = gameTime[i1]            
        gamedate = ET.SubElement(game, 'gamedate')
        print scheduleDates[i1]
        gamedate.text = scheduleDates[i1]
        status = ET.SubElement(game, 'status')
        status.text ='Pre-Game'
        displaystatus1=ET.SubElement(game, 'display_status1')
        displaystatus1.text = ''
        displaystatus2=ET.SubElement(game, 'display_status2')
        displaystatus2.text = ''
    scheduleXML = ET.tostring(score)
    print scheduleXML

    print   pathname +'scoringUpdate' + sport + '.xml'
    with open(pathname +'scoringUpdate' + sport + '.xml', 'w') as scoreData:
        scoreData.write(str(scheduleXML))
    return 
    
if __name__ == "__main__":
    
    sport = sys.argv[1]
    pathname = sys.argv[2]
    currentTime = time.strftime('%I:%M %p', time.localtime())
    currentDate = time.strftime('%m/%d/%Y', time.localtime())

    i=0
    url = ''
    errorFlag='0'
    
    scheduleDates,gamecodes,scheduleTime,gameTime,awayTeams,homeTeams,gameList,errorFlag = scrape(url,i,errorFlag)
    
    errorFlag=dataScrub(scheduleTime,gameTime,awayTeams,homeTeams,gameList,errorFlag)
                               
    makeSchedule(gameTime,homeTeams,awayTeams,scheduleTime,currentDate,currentTime,gamecodes, scheduleDates)
