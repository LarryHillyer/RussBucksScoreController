import pytz
import datetime
import time
import urllib2
import json
import os
import xml.etree.ElementTree as ET
import sys


def today(league1,date1,pathname):
  url = 'http://scores.nbcsports.msnbc.com/ticker/data/gamesMSNBC.js.asp?jsonp=true&sport=%s&period=%d'
  #yyyymmdd = int(datetime.datetime.now(pytz.timezone('US/Mountain')).strftime("%Y%m%d"))
  yyyymmdd= int(date1)
  games = []
  try:
    currentTime = time.strftime('%I:%M %p', time.localtime())
    currentDate = time.strftime('%m/%d/%y', time.localtime())
    try:
      f = urllib2.urlopen(url % (league1, yyyymmdd))
      jsonp = f.read()
      f.close() 
      count = 0
      json_str = jsonp.replace('shsMSNBCTicker.loadGamesData(', '').replace(');', '')
      json_parsed = json.loads(json_str)
    except Exception, e:
      print e
      print 'Just Got bit by a snake'
      time.sleep(10)
      f = urllib2.urlopen(url % (league1, yyyymmdd))
      jsonp = f.read()
      f.close()
      count = 0
      json_str = jsonp.replace('shsMSNBCTicker.loadGamesData(', '').replace(');', '')
      json_parsed = json.loads(json_str)
      
    for game_str in json_parsed.get('games', []):
      count += 1
      game_tree = ET.XML(game_str)
      gamecode = game_tree.get('gamecode')
      visiting_tree = game_tree.find('visiting-team')
      home_tree = game_tree.find('home-team')
      gamestate_tree = game_tree.find('gamestate')
      home = home_tree.get('nickname')
      away = visiting_tree.get('nickname')
      visitingScore = visiting_tree.get('score')
      homeTeamScore = home_tree.get('score')
      os.environ['TZ'] = 'US/Eastern'
      start = int(
        time.mktime(time.strptime('%s %d' % (gamestate_tree.get('gametime'), yyyymmdd), '%I:%M %p %Y%m%d')))
      del os.environ['TZ']

      if visitingScore:
        pass
      else:
        visitingScore = '0'

      if homeTeamScore:
        pass
      else:
        homeTeamScore = '0'
        
      if count == 1:
        score = ET.Element('score')
        score.attrib['filedate'] = currentDate
        score.attrib['filetime'] = currentTime
        game = ET.SubElement(score, 'game')
        game.attrib['hometeam'] = home
        game.attrib['awayteam'] = away
        game.attrib['gamecode'] = gamecode
        awayscore = ET.SubElement(game, 'awayscore')
        awayscore.text = visitingScore
        homescore = ET.SubElement(game, 'homescore')
        homescore.text = homeTeamScore
        gametime = ET.SubElement(game, 'gametime')
        gametime.text = gamestate_tree.get('gametime') 
        gamedate = ET.SubElement(game, 'gamedate')
        gamedate.text = gamestate_tree.get('gamedate')
        status = ET.SubElement(game, 'status')  
        status.text = gamestate_tree.get('status')
        displaystatus1=ET.SubElement(game, 'display_status1')
        displaystatus1.text = gamestate_tree.get('display_status1')
        displaystatus2=ET.SubElement(game, 'display_status2')
        displaystatus2.text = gamestate_tree.get('display_status2')
        
      else:
        game = ET.Element('game')
        game.attrib['hometeam'] = home
        game.attrib['awayteam'] = away
        game.attrib['gamecode'] = gamecode
        awayscore = ET.SubElement(game, 'awayscore')
        awayscore.text = visitingScore
        homescore = ET.SubElement(game, 'homescore')
        homescore.text = homeTeamScore
        gametime = ET.SubElement(game,'gametime')
        gametime.text = gamestate_tree.get('gametime')
        gamedate = ET.SubElement(game, 'gamedate')
        gamedate.text = gamestate_tree.get('gamedate')
        status = ET.SubElement(game, 'status')
        status.text = gamestate_tree.get('status')
        displaystatus1=ET.SubElement(game, 'display_status1')
        displaystatus1.text = gamestate_tree.get('display_status1')
        displaystatus2=ET.SubElement(game, 'display_status2')
        displaystatus2.text = gamestate_tree.get('display_status2')        
        score.append(game)
      gameXML = ET.tostring(score)
    with open(pathname + 'scoringUpdate' + sport + '.xml', 'w') as scoreData:
      scoreData.write(str(gameXML))
  except Exception, e:
    print e

if __name__ == "__main__":
  league1 = sys.argv[1]
  sport = sys.argv[2]
  pathname = sys.argv[4]
  print sys.argv[4]
  date1a = time.strptime(sys.argv[3], '%m/%d/%y')
  date1 = time.strftime('%Y%m%d',date1a)
  today(league1,date1,pathname)

