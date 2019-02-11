# Do not delete the following import lines
from abaqus import *
from abaqusConstants import *
import __main__

def Macro1():
    import section
    import regionToolset
    import displayGroupMdbToolset as dgm
    import part
    import material
    import assembly
    import step
    import interaction
    import load
    import mesh
    import optimization
    import job
    import sketch
    import visualization
    import xyPlot
    import displayGroupOdbToolset as dgo
    import connectorBehavior
    iges = mdb.openIges('@StampFolder', msbo=False, 
        trimCurve=DEFAULT, topology=SHELL, scaleFromFile=OFF)
    mdb.models['Model-1'].PartFromGeometryFile(name='Stamp', geometryFile=iges, 
        combine=False, stitchAfterCombine=False, stitchTolerance=1.0, 
        dimensionality=THREE_D, type=DISCRETE_RIGID_SURFACE, topology=SHELL, 
        convertToAnalytical=1, stitchEdges=1)
    p = mdb.models['Model-1'].parts['Stamp']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    iges = mdb.openIges('@BlankFolder', msbo=False, 
        trimCurve=DEFAULT, scaleFromFile=OFF)
    mdb.models['Model-1'].PartFromGeometryFile(name='Blank', geometryFile=iges, 
        combine=False, stitchAfterCombine=False, stitchTolerance=1.0, 
        dimensionality=THREE_D, type=DEFORMABLE_BODY, convertToAnalytical=1, 
        stitchEdges=1)
    p = mdb.models['Model-1'].parts['Blank']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    iges = mdb.openIges('@PlatformFolder', msbo=False, 
        trimCurve=DEFAULT, topology=SHELL, scaleFromFile=OFF)
    mdb.models['Model-1'].PartFromGeometryFile(name='Platform', geometryFile=iges, 
        combine=False, stitchAfterCombine=False, stitchTolerance=1.0, 
        dimensionality=THREE_D, type=DISCRETE_RIGID_SURFACE, topology=SHELL, 
        convertToAnalytical=1, stitchEdges=1)
    p = mdb.models['Model-1'].parts['Platform']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    p = mdb.models['Model-1'].parts['Stamp']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    p = mdb.models['Model-1'].parts['Stamp']
    p.ReferencePoint(point=(0.0, 60.0, 0.0))
    mdb.models['Model-1'].parts['Stamp'].features.changeKey(fromName='RP', 
        toName='StampRP')
    p = mdb.models['Model-1'].parts['Stamp']
    r = p.referencePoints
    refPoints=(r[2], )
    p.Set(referencePoints=refPoints, name='Set-1')
    session.viewports['Viewport: 1'].view.setValues(nearPlane=42.9828, 
        farPlane=73.6265, width=40.6961, height=21.6161, cameraPosition=(
        6.99108, -46.4445, -12.8341), cameraUpVector=(0.18151, 0.140899, 
        0.973243), cameraTarget=(0.175544, 9.6484, 0.175546))
    session.viewports['Viewport: 1'].view.setValues(nearPlane=43.1381, 
        farPlane=73.4978, width=40.8431, height=21.6942, cameraPosition=(
        7.90963, -46.8577, -10.2842), cameraUpVector=(0.150481, 0.182546, 
        0.971613), cameraTarget=(0.1806, 9.64613, 0.18958))
    p = mdb.models['Model-1'].parts['Stamp']
    s = p.faces
    side1Faces = s.getSequenceFromMask(mask=('[#1ff2 ]', ), )
    #without fillet in stamp upper radius
    #side1Faces = s.getSequenceFromMask(mask=('[#7da ]', ), )
    #cube
    #side1Faces = s.getSequenceFromMask(mask=('[#1e0 ]', ), )
    #one of them was with cube inside stamp
    # side1Faces = s.getSequenceFromMask(mask=('[#7c2 ]', ), )
    # side1Faces = s.getSequenceFromMask(mask=('[#6c2 ]', ), )
    p.Surface(side1Faces=side1Faces, name='StampInside')
    p = mdb.models['Model-1'].parts['Blank']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    session.viewports['Viewport: 1'].view.setValues(nearPlane=34.7859, 
        farPlane=54.2631, width=34.3125, height=18.2254, cameraPosition=(
        20.7216, 13.5992, 39.1397), cameraUpVector=(-0.272086, 0.899699, 
        -0.341338), cameraTarget=(0.00951886, 8.98195, 0.00905848))
    p = mdb.models['Model-1'].parts['Blank']
    c = p.cells
    cells = c.getSequenceFromMask(mask=('[#1 ]', ), )
    p.Set(cells=cells, name='Set-1')
    session.viewports['Viewport: 1'].view.setValues(nearPlane=35.7225, 
        farPlane=53.328, width=35.2364, height=18.7161, cameraPosition=(13.971, 
        10.328, 42.2559), cameraUpVector=(-0.356832, 0.896711, -0.261877), 
        cameraTarget=(0.00796235, 8.9812, 0.00977707))
    p = mdb.models['Model-1'].parts['Blank']
    s = p.faces
    side1Faces = s.getSequenceFromMask(mask=('[#3f ]', ), )
    p.Surface(side1Faces=side1Faces, name='Surf-1')
    p = mdb.models['Model-1'].parts['Blank']
    s = p.faces
    side1Faces = s.getSequenceFromMask(mask=('[#15 ]', ), )
    p.Surface(side1Faces=side1Faces, name='BlankFillet')
    p = mdb.models['Model-1'].parts['Blank']
    s = p.faces
    side1Faces = s.getSequenceFromMask(mask=('[#2a ]', ), )
    p.Surface(side1Faces=side1Faces, name='OtherParts')
    p = mdb.models['Model-1'].parts['Platform']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    p = mdb.models['Model-1'].parts['Platform']
    p.ReferencePoint(point=(0.0, 1.0, 0.0))
    mdb.models['Model-1'].parts['Platform'].features.changeKey(fromName='RP', 
        toName='PlatformRP')
    p = mdb.models['Model-1'].parts['Platform']
    r = p.referencePoints
    refPoints=(r[2], )
    p.Set(referencePoints=refPoints, name='Set-1')
    p = mdb.models['Model-1'].parts['Platform']
    s = p.faces
    side1Faces = s.getSequenceFromMask(mask=('[#4 ]', ), )
    p.Surface(side1Faces=side1Faces, name='Surf-1')
    mdb.models['Model-1'].parts['Platform'].surfaces.changeKey(fromName='Surf-1', 
        toName='PlatformTop')
    p = mdb.models['Model-1'].parts['Blank']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    session.viewports['Viewport: 1'].partDisplay.setValues(sectionAssignments=ON, 
        engineeringFeatures=ON)
    session.viewports['Viewport: 1'].partDisplay.geometryOptions.setValues(
        referenceRepresentation=OFF)
    mdb.models['Model-1'].Material(name='MaterialName')
    mdb.models['Model-1'].materials['MaterialName'].Density(table=((@materialDensity, ), ))
    mdb.models['Model-1'].materials['MaterialName'].Elastic(table=((@YoungsModulus, @PoissonsRatio), ))
    mdb.models['Model-1'].materials['MaterialName'].Plastic(table=((@Plastic)))
    mdb.models['Model-1'].HomogeneousSolidSection(name='BlankMaterial', 
        material='MaterialName', thickness=None)
    p = mdb.models['Model-1'].parts['Blank']
    region = p.sets['Set-1']
    p = mdb.models['Model-1'].parts['Blank']
    p.SectionAssignment(region=region, sectionName='BlankMaterial', offset=0.0, 
        offsetType=MIDDLE_SURFACE, offsetField='', 
        thicknessAssignment=FROM_SECTION)
    a = mdb.models['Model-1'].rootAssembly
    session.viewports['Viewport: 1'].setValues(displayedObject=a)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(
        optimizationTasks=OFF, geometricRestrictions=OFF, stopConditions=OFF)
    a = mdb.models['Model-1'].rootAssembly
    a.DatumCsysByDefault(CARTESIAN)
    p = mdb.models['Model-1'].parts['Stamp']
    a.Instance(name='Stamp-1', part=p, dependent=OFF)
    a = mdb.models['Model-1'].rootAssembly
    p = mdb.models['Model-1'].parts['Blank']
    a.Instance(name='Blank-1', part=p, dependent=OFF)
    a = mdb.models['Model-1'].rootAssembly
    p = mdb.models['Model-1'].parts['Platform']
    a.Instance(name='Platform-1', part=p, dependent=OFF)
    a = mdb.models['Model-1'].rootAssembly
    a.rotate(instanceList=('Stamp-1', ), axisPoint=(0.0, 0.0, 0.0), axisDirection=(
        0.1, 0.0, 0.0), angle=270.0)
    a = mdb.models['Model-1'].rootAssembly
    a.rotate(instanceList=('Blank-1', ), axisPoint=(0.0, 0.0, 0.0), axisDirection=(
        0.1, 0.0, 0.0), angle=270.0)
    a = mdb.models['Model-1'].rootAssembly
    a.rotate(instanceList=('Platform-1', ), axisPoint=(0.0, 0.0, 0.0), 
        axisDirection=(0.1, 0.0, 0.0), angle=270.0)
    a = mdb.models['Model-1'].rootAssembly
    a.translate(instanceList=('Platform-1', ), vector=(0.0, 0.0, @blankPosition + 1))
    a = mdb.models['Model-1'].rootAssembly
    a.translate(instanceList=('Blank-1', ), vector=(0.0, 0.0, @blankPosition))
    session.viewports['Viewport: 1'].view.setValues(nearPlane=56.7817, 
        farPlane=105.915, width=50.1656, height=26.6459, cameraPosition=(
        29.4118, 68.8921, 31.9056), cameraUpVector=(-0.653783, 0.302114, 
        -0.693754), cameraTarget=(-0.59442, 10.4981, -0.403634))
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(
        adaptiveMeshConstraints=ON)
    mdb.models['Model-1'].ExplicitDynamicsStep(name='Crash', previous='Initial', 
        timePeriod=0.05)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(step='Crash')
    mdb.models['Model-1'].fieldOutputRequests['F-Output-1'].setValues(
        numIntervals=60)
    mdb.models['Model-1'].FieldOutputRequest(name='F-Output-2', 
        createStepName='Crash', variables=('EVOL', ))
    regionDef=mdb.models['Model-1'].rootAssembly.instances['Stamp-1'].sets['Set-1']
    mdb.models['Model-1'].FieldOutputRequest(name='F-Output-3', 
        createStepName='Crash', variables=('U', ), region=regionDef, 
        sectionPoints=DEFAULT, rebar=EXCLUDE)
    regionDef=mdb.models['Model-1'].rootAssembly.instances['Blank-1'].sets['Set-1']
    mdb.models['Model-1'].FieldOutputRequest(name='F-Output-4', 
        createStepName='Crash', variables=('COORD', ), region=regionDef, 
        sectionPoints=DEFAULT, rebar=EXCLUDE)
    regionDef=mdb.models['Model-1'].rootAssembly.instances['Platform-1'].sets['Set-1']
    mdb.models['Model-1'].HistoryOutputRequest(name='H-Output-2', 
        createStepName='Crash', variables=('RF1', 'RF2', 'RF3'), 
        region=regionDef, sectionPoints=DEFAULT, rebar=EXCLUDE)
    regionDef=mdb.models['Model-1'].rootAssembly.instances['Stamp-1'].sets['Set-1']
    mdb.models['Model-1'].HistoryOutputRequest(name='H-Output-3', 
        createStepName='Crash', variables=('RF1', 'RF2', 'RF3','U3'), 
        region=regionDef, sectionPoints=DEFAULT, rebar=EXCLUDE)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(interactions=ON, 
        constraints=ON, connectors=ON, engineeringFeatures=ON, 
        adaptiveMeshConstraints=OFF)
    mdb.models['Model-1'].ContactProperty('IntProp-1')
    mdb.models['Model-1'].interactionProperties['IntProp-1'].TangentialBehavior(
        formulation=PENALTY, directionality=ISOTROPIC, slipRateDependency=OFF, 
        pressureDependency=OFF, temperatureDependency=OFF, dependencies=0, 
        table=((@Friction, ), ), shearStressLimit=None, maximumElasticSlip=FRACTION, 
        fraction=0.005, elasticSlipStiffness=None)
    mdb.models['Model-1'].interactionProperties['IntProp-1'].NormalBehavior(
        pressureOverclosure=HARD, allowSeparation=ON, 
        constraintEnforcementMethod=DEFAULT)
    a = mdb.models['Model-1'].rootAssembly
    region1=a.instances['Stamp-1'].surfaces['StampInside']
    a = mdb.models['Model-1'].rootAssembly
    region2=a.instances['Blank-1'].surfaces['Surf-1']
    mdb.models['Model-1'].SurfaceToSurfaceContactExp(name ='Stamp&Blank', 
        createStepName='Crash', master = region1, slave = region2, 
        mechanicalConstraint=KINEMATIC, sliding=FINITE, 
        interactionProperty='IntProp-1', initialClearance=OMIT, datumAxis=None, 
        clearanceRegion=None)
    a = mdb.models['Model-1'].rootAssembly
    region1=a.instances['Platform-1'].surfaces['PlatformTop']
    a = mdb.models['Model-1'].rootAssembly
    region2=a.instances['Blank-1'].surfaces['Surf-1']
    mdb.models['Model-1'].SurfaceToSurfaceContactExp(name ='Platform&Blank', 
        createStepName='Crash', master = region1, slave = region2, 
        mechanicalConstraint=KINEMATIC, sliding=FINITE, 
        interactionProperty='IntProp-1', initialClearance=OMIT, datumAxis=None, 
        clearanceRegion=None)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(loads=ON, bcs=ON, 
        predefinedFields=ON, interactions=OFF, constraints=OFF, 
        engineeringFeatures=OFF)
    mdb.models['Model-1'].TabularAmplitude(name='Amp-1', timeSpan=STEP, 
        smooth=SOLVER_DEFAULT, data=((0.0, 0.0), (1.0, 1.0)))
    a = mdb.models['Model-1'].rootAssembly
    region = a.instances['Platform-1'].sets['Set-1']
    mdb.models['Model-1'].DisplacementBC(name='BottomFix', createStepName='Crash', 
        region=region, u1=0.0, u2=0.0, u3=0.0, ur1=0.0, ur2=0.0, ur3=0.0, 
        amplitude='Amp-1', fixed=OFF, distributionType=UNIFORM, fieldName='', 
        localCsys=None)
    a = mdb.models['Model-1'].rootAssembly
    region = a.instances['Stamp-1'].sets['Set-1']
    mdb.models['Model-1'].DisplacementBC(name='TopFix', createStepName='Crash', 
        region=region, u1=0.0, u2=0.0, u3=UNSET, ur1=0.0, ur2=0.0, ur3=0.0, 
        amplitude='Amp-1', fixed=OFF, distributionType=UNIFORM, fieldName='', 
        localCsys=None)
    a = mdb.models['Model-1'].rootAssembly
    region = a.instances['Stamp-1'].sets['Set-1']
    mdb.models['Model-1'].DisplacementBC(name='TopMove', createStepName='Crash', 
        region=region, u1=UNSET, u2=UNSET, u3=@Displacement / 0.05, ur1=UNSET, ur2=UNSET, 
        ur3=UNSET, amplitude='Amp-1', fixed=OFF, distributionType=UNIFORM, 
        fieldName='', localCsys=None)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(mesh=ON, loads=OFF, 
        bcs=OFF, predefinedFields=OFF, connectors=OFF)
    session.viewports['Viewport: 1'].assemblyDisplay.meshOptions.setValues(
        meshTechnique=ON)
    elemType1 = mesh.ElemType(elemCode=C3D8R, elemLibrary=STANDARD, 
        kinematicSplit=AVERAGE_STRAIN, secondOrderAccuracy=OFF, 
        hourglassControl=DEFAULT, distortionControl=DEFAULT)
    elemType2 = mesh.ElemType(elemCode=C3D6, elemLibrary=STANDARD)
    elemType3 = mesh.ElemType(elemCode=C3D4, elemLibrary=STANDARD)
    a = mdb.models['Model-1'].rootAssembly
    c1 = a.instances['Blank-1'].cells
    cells1 = c1.getSequenceFromMask(mask=('[#1 ]', ), )
    pickedRegions =(cells1, )
    a.setElementType(regions=pickedRegions, elemTypes=(elemType1, elemType2, 
        elemType3))
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['Stamp-1'], )
    a.seedPartInstance(regions=partInstances, size=5.0, deviationFactor=0.1, 
        minSizeFactor=0.1)
    # a = mdb.models['Model-1'].rootAssembly
    # partInstances =(a.instances['Blank-1'], )
    # a.seedPartInstance(regions=partInstances, size=@Mesh, deviationFactor=0.1, 
    #     minSizeFactor=0.1)
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['Platform-1'], )
    a.seedPartInstance(regions=partInstances, size=20, deviationFactor=0.1, 
        minSizeFactor=0.1)
    #
    #creating mesh for blank
    #
    a = mdb.models['Model-1'].rootAssembly
    session.viewports['Viewport: 1'].setValues(displayedObject=a)
    a1 = mdb.models['Model-1'].rootAssembly
    a1.regenerate()
    a = mdb.models['Model-1'].rootAssembly
    e1 = a.instances['Blank-1'].edges
    pickedEdges = e1.getSequenceFromMask(mask=('[#bffffff #2000c00 ]', ), )
    a.seedEdgeBySize(edges=pickedEdges, size=5, deviationFactor=0.1, 
        constraint=FINER)
    a = mdb.models['Model-1'].rootAssembly
    e1 = a.instances['Blank-1'].edges
    pickedEdges1 = e1.getSequenceFromMask(mask=('[#c000000 #3fff000 ]', ), )
    pickedEdges2 = e1.getSequenceFromMask(mask=('[#f0000000 #3ff ]', ), )
    a.seedEdgeByBias(biasMethod=SINGLE, end1Edges=pickedEdges1, 
        end2Edges=pickedEdges2, minSize=@Mesh, maxSize=5, constraint=FINER)
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['Blank-1'], )
    a.generateMesh(regions=partInstances)
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['Stamp-1'], )
    a.generateMesh(regions=partInstances)
    # a = mdb.models['Model-1'].rootAssembly
    # partInstances =(a.instances['Blank-1'], )
    # a.generateMesh(regions=partInstances)
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['Platform-1'], )
    a.generateMesh(regions=partInstances)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(mesh=OFF)
    session.viewports['Viewport: 1'].assemblyDisplay.meshOptions.setValues(
        meshTechnique=OFF)
    mdb.saveAs(pathName='@caeSave')
    mdb.Job(name='@Name', model='Model-1', description='', type=ANALYSIS, 
        atTime=None, waitMinutes=0, waitHours=0, queue=None, 
        explicitPrecision=SINGLE, nodalOutputPrecision=SINGLE, echoPrint=OFF, 
        modelPrint=OFF, contactPrint=OFF, historyPrint=OFF, userSubroutine='', 
        scratch='', parallelizationMethodExplicit=DOMAIN, numDomains=@Core, 
        activateLoadBalancing=False, multiprocessingMode=DEFAULT, numCpus=@Core)
    mdb.jobs['@Name'].submit(consistencyChecking=OFF)
Macro1()