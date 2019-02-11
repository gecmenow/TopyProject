from odbAccess import *

# Open the output database.
import os; 

#filepath = os.environ['USERPROFILE'] + '/Job-1.odb';
filepath = 'Job-1.odb';

#odb = openOdb(path='C:/Users/User/Job-1.odb')
odb = openOdb(filepath)

outputFile = open('outputs.txt','w')

for stepName in odb.steps.keys():
	print stepName
step1 = odb.steps['Crash'] 
print 'Text'
data = step1.historyRegions.keys()

for item in data:
  outputFile.write("%s\n" % item)
print data