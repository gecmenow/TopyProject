
from odbAccess import *

# Open the output database.
import os; 

#filepath = os.environ['USERPROFILE'] + '/Job-1.odb';
filepath = 'Job-1.odb';
#odb = openOdb(path='C:/Users/User/Job-1.odb')
odb = openOdb(filepath)

EVOLOutput = open('EVOLOutput.csv','w')

# SOutput = open('SOutput.csv','w')
# LEOutput = open('LEOutput.csv','w')
# AOutput = open('AOutput.csv','w')
# AROutput = open('AROutput.csv','w')
# PEOutput = open('PEOutput.csv','w')
# RFOutput = open('RFOutput.csv','w')
# RMOutput = open('RMOutput.csv','w')
COORDOutput = open('COORDOutput.csv','w')
# UROutput = open('UROutput.csv','w')
# VOutput = open('VOutput.csv','w')
# VROutput = open('VROutput.csv','w')
# VROutput = open('VROutput.csv','w')

assembly = odb.rootAssembly

# Model data output

# For each instance in the assembly.
for stepName in odb.steps.keys():
	lastFrame = odb.steps[stepName].frames[-1]

for fieldName in lastFrame.fieldOutputs.keys():
    print fieldName

for f in lastFrame.fieldOutputs.values():
    print f.name, ':', f.description
    print 'Type: ', f.type

    # For each location value, print the position.
  
    for loc in f.locations:
        print 'Position:',loc.position
    print


#firstFrame = odb.steps['Crash'].frames[0]
lastFrame = odb.steps['Crash'].frames[-1]

stress=lastFrame.fieldOutputs['S']
print stress.values[0].__members__
#lastFrame = odb.steps['Crash'].frames[-1]

# SOutput.write('Element;X;Y;Z\n')
# output=lastFrame.fieldOutputs['S']
# for out in output.values:
# 	SOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.elementLabel, out.data[0], out.data[1], out.data[2]))

# FirstCOORDOutput.write('Node;X;Y;Z\n')
# output=firstFrame.fieldOutputs['COORD']
# for out in output.values:
# 	FirstCOORDOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

# LastCOORDOutput.write('Node;X;Y;Z\n')
# output=lastFrame.fieldOutputs['COORD']
# for out in output.values:
# 	LastCOORDOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

# output=lastFrame.fieldOutputs['LE']
# LEOutput.write('Element;X;Y;Z\n')
# for out in output.values:
# 	LEOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.elementLabel, out.data[0], out.data[1], out.data[2]))

# output=lastFrame.fieldOutputs['A']
# AOutput.write('Node;X;Y;Z\n')
# for out in output.values:
# 	AOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

# output=lastFrame.fieldOutputs['AR']
# AROutput.write('Node;X;Y;Z\n')
# for out in output.values:
# 	AROutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

# output=lastFrame.fieldOutputs['PE']
# PEOutput.write('Element;X;Y;Z\n')
# for out in output.values:
# 	PEOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.elementLabel, out.data[0], out.data[1], out.data[2]))

output=lastFrame.fieldOutputs['EVOL']
for out in output.values:
	EVOLOutput.write('%d;%6.4f\n' % (out.elementLabel, out.data))

# output=lastFrame.fieldOutputs['RF']
# RFOutput.write('Node;X;Y;Z\n')
# for out in output.values:
# 	RFOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

# output=lastFrame.fieldOutputs['RM']
# RMOutput.write('Node;X;Y;Z\n')
# for out in output.values:
# 	RMOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

output=lastFrame.fieldOutputs['COORD']
COORDOutput.write('Node;X;Y;Z\n')
for out in output.values:
	COORDOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

# output=lastFrame.fieldOutputs['UR']
# UROutput.write('Node;X;Y;Z\n')
# for out in output.values:
# 	UROutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

# output=lastFrame.fieldOutputs['V']
# VOutput.write('Node;X;Y;Z\n')
# for out in output.values:
# 	VOutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

# output=lastFrame.fieldOutputs['VR']
# VROutput.write('Node;X;Y;Z\n')
# for out in output.values:
# 	VROutput.write('%d;%6.4f;%6.4f;%6.4f\n' % (out.nodeLabel, out.data[0], out.data[1], out.data[2]))

for stepName in odb.steps.keys():
	print stepName
step1 = odb.steps['Crash'] 
print 'Text'
print step1.historyRegions.keys() 

region = step1.historyRegions['@LowerPlateName']
#for stepName in odb.steps.keys():
print region.historyOutputs.keys()
RF1LOWERPLATE = region.historyOutputs['RF1'].data
#print RF1LOWERPLATE.keys()
csvFile = open('RF1LOWERPLATE.csv','w')
for time, force in RF1LOWERPLATE:
    csvFile.write('%6.4f;%6.4f\n' % (time, force))
csvFile.close()

RF2LOWERPLATE = region.historyOutputs['RF2'].data
csvFile = open('RF2LOWERPLATE.csv','w')
for time, force in RF2LOWERPLATE:
    csvFile.write('%6.4f;%6.4f\n' % (time, force))
csvFile.close()

RF3LOWERPLATE = region.historyOutputs['RF3'].data
csvFile = open('RF3LOWERPLATE.csv','w')
for time, force in RF3LOWERPLATE:
    csvFile.write('%6.4f;%6.4f\n' % (time, force))
csvFile.close()

region = step1.historyRegions['@UpperPlateName']
print region.historyOutputs.keys()

RF1Stamp = region.historyOutputs['RF1'].data
csvFile = open('RF1Stamp.csv','w')
for time, force in RF1Stamp:
    csvFile.write('%6.4f;%6.4f\n' % (time, force))
csvFile.close()

RF2Stamp = region.historyOutputs['RF2'].data
csvFile = open('RF2Stamp.csv','w')
for time, force in RF2Stamp:
    csvFile.write('%6.4f;%6.4f\n' % (time, force))
csvFile.close()

RF3Stamp = region.historyOutputs['RF3'].data
csvFile = open('RF3Stamp.csv','w')
for time, force in RF3Stamp:
    csvFile.write('%6.4f;%6.4f\n' % (time, force))
csvFile.close()

U3Stamp = region.historyOutputs['U3'].data
csvFile = open('U3Stamp.csv','w')
for time, move in U3Stamp:
    csvFile.write('%6.4f;%6.4f\n' % (time, move))
csvFile.close()