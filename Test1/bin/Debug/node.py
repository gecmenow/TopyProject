from odbAccess import *
import sys
from abaqusConstants import *
from decimal import *

from odbMaterial import *
from odbSection import *

#odbPath = os.environ['USERPROFILE'] + '/Job-1.odb';
odbPath = 'Job-1.odb'
# Open the output database.

odb = openOdb(path=odbPath)

blank = open('blankNodes.csv','w')
stamp = open('stampCoordinates.csv','w')
blankElementsCoordinates = open('blankElementsCoordinates.csv', 'w')

blank.write('Node;X;Y;Z\n')

assembly = odb.rootAssembly

# Model data output

print 'Model data for ODB: ', odbPath

# For each instance in the assembly.

numNodes = numElements = 0

for name, instance in assembly.instances.items():

    n = len(instance.nodes)
    print 'Number of nodes of instance %s: %d' % (name, n)
    numNodes = numNodes + n

    print
    print 'NODAL COORDINATES'

    # For each node of each part instance
    # print the node label and the nodal coordinates.
    # Three-dimensional parts include X-, Y-, and Z-coordinates.
    # Two-dimensional parts include X- and Y-coordinates.

    for node in instance.nodes:
        if name == 'BLANK-1':
            print node.coordinates
            blank.write('%d;%6.4f;%6.4f;%6.4f\n' % (node.label, node.coordinates[0], node.coordinates[1], node.coordinates[2]))
        if name == 'STAMP-1':
            print node.coordinates
            stamp.write('%d;%6.4f;%6.4f;%6.4f\n' % (node.label,node.coordinates[0],node.coordinates[1], node.coordinates[2]))
        
             
    # For each element of each part instance
    # print the element label, the element type, the
    # number of nodes, and the element connectivity.
       
    n = len(instance.elements)
    print 'Number of elements of instance ', name, ': ', n
    numElements = numElements + n

    print 'ELEMENT CONNECTIVITY'
    print ' Number  Type    Connectivity'
    for element in instance.elements:
        if name == 'BLANK-1':
            blankElementsCoordinates.write('\n%5d;' % (element.label)),
            for nodeNum in element.connectivity:
               blankElementsCoordinates.write('%4d;' % nodeNum),
        print
   
print
print 'Number of instances: ', len(assembly.instances)
print 'Total number of elements: ', numElements
print 'Total number of nodes: ', numNodes