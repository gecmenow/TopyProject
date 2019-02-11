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
    s = mdb.models['Model-1'].ConstrainedSketch(name='__profile__', 
        sheetSize=200.0)
    g, v, d, c = s.geometry, s.vertices, s.dimensions, s.constraints
    s.setPrimaryObject(option=STANDALONE)
    s.Spot(point=(-10.0, 10.0))
    s.Spot(point=(10.0, 10.0))
    s.Spot(point=(10.0, -10.0))
    s.Spot(point=(-10.0, -10.0))
    s.Line(point1=(-10.0, 10.0), point2=(10.0, 10.0))
    s.HorizontalConstraint(entity=g[2], addUndoState=False)
    s.Line(point1=(10.0, 10.0), point2=(10.0, -10.0))
    s.VerticalConstraint(entity=g[3], addUndoState=False)
    s.PerpendicularConstraint(entity1=g[2], entity2=g[3], addUndoState=False)
    s.Line(point1=(10.0, -10.0), point2=(-10.0, -10.0))
    s.HorizontalConstraint(entity=g[4], addUndoState=False)
    s.PerpendicularConstraint(entity1=g[3], entity2=g[4], addUndoState=False)
    s.Line(point1=(-10.0, -10.0), point2=(-10.0, 10.0))
    s.VerticalConstraint(entity=g[5], addUndoState=False)
    s.PerpendicularConstraint(entity1=g[4], entity2=g[5], addUndoState=False)
    p = mdb.models['Model-1'].Part(name='UpperPlate', dimensionality=THREE_D, 
        type=DISCRETE_RIGID_SURFACE)
    p = mdb.models['Model-1'].parts['UpperPlate']
    p.BaseSolidExtrude(sketch=s, depth=20.0)
    s.unsetPrimaryObject()
    p = mdb.models['Model-1'].parts['UpperPlate']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    del mdb.models['Model-1'].sketches['__profile__']
    p = mdb.models['Model-1'].parts['UpperPlate']
    f, e = p.faces, p.edges
    t = p.MakeSketchTransform(sketchPlane=f[4], sketchUpEdge=e[0], 
        sketchPlaneSide=SIDE1, sketchOrientation=RIGHT, origin=(0.0, 0.0, 
        20.0))
    s1 = mdb.models['Model-1'].ConstrainedSketch(name='__profile__', 
        sheetSize=69.28, gridSpacing=1.73, transform=t)
    g, v, d, c = s1.geometry, s1.vertices, s1.dimensions, s1.constraints
    s1.setPrimaryObject(option=SUPERIMPOSE)
    p = mdb.models['Model-1'].parts['UpperPlate']
    p.projectReferencesOntoSketch(sketch=s1, filter=COPLANAR_EDGES)
    s1.CircleByCenterPerimeter(center=(0.0, 0.0), point1=(3.8925, -6.055))
    s1.RadialDimension(curve=g[6], textPoint=(-7.78661251068115, 
        -7.91850852966309), radius=@DieRadius)
    p = mdb.models['Model-1'].parts['UpperPlate']
    f1, e1 = p.faces, p.edges
    p.CutExtrude(sketchPlane=f1[4], sketchUpEdge=e1[0], sketchPlaneSide=SIDE1, 
        sketchOrientation=RIGHT, sketch=s1, depth=18.0, 
        flipExtrudeDirection=OFF)
    s1.unsetPrimaryObject()
    del mdb.models['Model-1'].sketches['__profile__']
    p = mdb.models['Model-1'].parts['UpperPlate']
    f = p.faces
    p.DatumPlaneByOffset(plane=f[6], flip=SIDE1, offset=0.1)
    session.viewports['Viewport: 1'].view.setValues(nearPlane=55.1179, 
        farPlane=91.7749, width=48.6678, height=25.8648, cameraPosition=(
        19.0993, 14.4293, 79.4928), cameraUpVector=(-0.329437, 0.831326, 
        -0.447626), cameraTarget=(0.428885, -0.828515, 10.409))
    session.viewports['Viewport: 1'].view.setValues(nearPlane=53.1469, 
        farPlane=93.8426, width=46.9276, height=24.9399, cameraPosition=(
        36.9616, 16.7351, 71.3377), cameraUpVector=(-0.481849, 0.805926, 
        -0.343955), cameraTarget=(0.495915, -0.819862, 10.3784))
    p = mdb.models['Model-1'].parts['UpperPlate']
    f1 = p.faces
    p.DatumPlaneByOffset(plane=f1[1], flip=SIDE1, offset=0.01)
    p = mdb.models['Model-1'].parts['UpperPlate']
    f, e, d1 = p.faces, p.edges, p.datums
    t = p.MakeSketchTransform(sketchPlane=d1[3], sketchUpEdge=e[2], 
        sketchPlaneSide=SIDE1, sketchOrientation=RIGHT, origin=(0.0, 0.0, 
        20.1))
    s = mdb.models['Model-1'].ConstrainedSketch(name='__profile__', 
        sheetSize=74.08, gridSpacing=1.85, transform=t)
    g, v, d, c = s.geometry, s.vertices, s.dimensions, s.constraints
    s.setPrimaryObject(option=SUPERIMPOSE)
    p = mdb.models['Model-1'].parts['UpperPlate']
    p.projectReferencesOntoSketch(sketch=s, filter=COPLANAR_EDGES)
    s.CircleByCenterPerimeter(center=(0.0, 0.0), point1=(2.775, -5.55))
    # lower radius
    s.RadialDimension(curve=g[2], textPoint=(-5.14555358886719, -5.77499961853027), 
        radius=@DieRadius)
    p = mdb.models['Model-1'].parts['UpperPlate']
    f1, e1, d2 = p.faces, p.edges, p.datums
    p.Wire(sketchPlane=d2[3], sketchUpEdge=e1[2], sketchPlaneSide=SIDE1, 
        sketchOrientation=RIGHT, sketch=s)
    s.unsetPrimaryObject()
    del mdb.models['Model-1'].sketches['__profile__']
    p = mdb.models['Model-1'].parts['UpperPlate']
    f, e, d1 = p.faces, p.edges, p.datums
    t = p.MakeSketchTransform(sketchPlane=d1[4], sketchUpEdge=e[3], 
        sketchPlaneSide=SIDE1, sketchOrientation=RIGHT, origin=(0.0, 0.0, 
        2.01))
    s1 = mdb.models['Model-1'].ConstrainedSketch(name='__profile__', 
        sheetSize=74.08, gridSpacing=1.85, transform=t)
    g, v, d, c = s1.geometry, s1.vertices, s1.dimensions, s1.constraints
    s1.setPrimaryObject(option=SUPERIMPOSE)
    p = mdb.models['Model-1'].parts['UpperPlate']
    p.projectReferencesOntoSketch(sketch=s1, filter=COPLANAR_EDGES)
    s1.CircleByCenterPerimeter(center=(0.0, 0.0), point1=(1.85, -3.2375))
	# upper radius
    s1.RadialDimension(curve=g[2], textPoint=(-3.51559638977051, 
        -4.11588335037231), radius=3.0)
    p = mdb.models['Model-1'].parts['UpperPlate']
    f1, e1, d2 = p.faces, p.edges, p.datums
    p.Wire(sketchPlane=d2[4], sketchUpEdge=e1[3], sketchPlaneSide=SIDE1, 
        sketchOrientation=RIGHT, sketch=s1)
    s1.unsetPrimaryObject()
    del mdb.models['Model-1'].sketches['__profile__']
    session.viewports['Viewport: 1'].view.setValues(nearPlane=56.9811, 
        farPlane=90.0424, width=50.3131, height=26.7392, cameraPosition=(
        12.2161, 6.83622, 82.2229), cameraUpVector=(-0.325583, 0.866244, 
        -0.378969), cameraTarget=(0.386809, -0.863507, 10.4264))
    p = mdb.models['Model-1'].parts['UpperPlate']
    f, e, d1 = p.faces, p.edges, p.datums
    p.ShellLoft(loftsections=((e[0], ), (e[1], )), startCondition=NONE, 
        endCondition=NONE)
    session.viewports['Viewport: 1'].view.setValues(nearPlane=56.7396, 
        farPlane=90.2839, width=50.0999, height=26.6259, cameraUpVector=(
        -0.0281658, 0.90155, -0.431758), cameraTarget=(0.386809, -0.863507, 
        10.4264))
    session.viewports['Viewport: 1'].view.setValues(nearPlane=51.3855, 
        farPlane=95.3844, width=45.3724, height=24.1134, cameraPosition=(
        46.2423, 28.6816, 59.2971), cameraUpVector=(-0.293464, 0.703602, 
        -0.647166), cameraTarget=(0.544647, -0.762173, 10.3201))
    session.viewports['Viewport: 1'].view.setValues(nearPlane=51.3309, 
        farPlane=95.2931, width=45.3242, height=24.0878, cameraPosition=(
        46.7398, 34.5573, 54.7348), cameraUpVector=(-0.431683, 0.654772, 
        -0.620422), cameraTarget=(0.5461, -0.745019, 10.3068))
    p = mdb.models['Model-1'].parts['UpperPlate']
    c1 = p.cells
    p.RemoveCells(cellList = c1[0:1])
    s = mdb.models['Model-1'].ConstrainedSketch(name='__profile__', 
        sheetSize=200.0)
    g, v, d, c = s.geometry, s.vertices, s.dimensions, s.constraints
    s.setPrimaryObject(option=STANDALONE)
    s.CircleByCenterPerimeter(center=(0.0, 0.0), point1=(0.0, -5.0))
    s.RadialDimension(curve=g[2], textPoint=(-7.45732498168945, -7.05801010131836), 
        radius=@blankRadius)
    p = mdb.models['Model-1'].Part(name='Blank', dimensionality=THREE_D, 
        type=DEFORMABLE_BODY)
    p = mdb.models['Model-1'].parts['Blank']
    p.BaseSolidExtrude(sketch=s, depth=@blankHeight)
    s.unsetPrimaryObject()
    p = mdb.models['Model-1'].parts['Blank']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    del mdb.models['Model-1'].sketches['__profile__']
    s1 = mdb.models['Model-1'].ConstrainedSketch(name='__profile__', 
        sheetSize=200.0)
    g, v, d, c = s1.geometry, s1.vertices, s1.dimensions, s1.constraints
    s1.setPrimaryObject(option=STANDALONE)
    s1.Spot(point=(-10.0, 10.0))
    s1.Spot(point=(10.0, 10.0))
    s1.Spot(point=(10.0, -10.0))
    s1.Spot(point=(-10.0, -10.0))
    s1.Line(point1=(-10.0, 10.0), point2=(10.0, 10.0))
    s1.HorizontalConstraint(entity=g[2], addUndoState=False)
    s1.Line(point1=(10.0, 10.0), point2=(10.0, -10.0))
    s1.VerticalConstraint(entity=g[3], addUndoState=False)
    s1.PerpendicularConstraint(entity1=g[2], entity2=g[3], addUndoState=False)
    s1.Line(point1=(10.0, -10.0), point2=(-10.0, -10.0))
    s1.HorizontalConstraint(entity=g[4], addUndoState=False)
    s1.PerpendicularConstraint(entity1=g[3], entity2=g[4], addUndoState=False)
    s1.Line(point1=(-10.0, -10.0), point2=(-10.0, 10.0))
    s1.VerticalConstraint(entity=g[5], addUndoState=False)
    s1.PerpendicularConstraint(entity1=g[4], entity2=g[5], addUndoState=False)
    p = mdb.models['Model-1'].Part(name='LowerPlate', dimensionality=THREE_D, 
        type=DISCRETE_RIGID_SURFACE)
    p = mdb.models['Model-1'].parts['LowerPlate']
    p.BaseShell(sketch=s1)
    s1.unsetPrimaryObject()
    p = mdb.models['Model-1'].parts['LowerPlate']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    del mdb.models['Model-1'].sketches['__profile__']
    p = mdb.models['Model-1'].parts['LowerPlate']
    p.ReferencePoint(point=(0.0, 0.0, 0.0))
    p = mdb.models['Model-1'].parts['UpperPlate']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    p = mdb.models['Model-1'].parts['UpperPlate']
    p.ReferencePoint(point=(0.0, 0.0, 0.0))
    p1 = mdb.models['Model-1'].parts['LowerPlate']
    session.viewports['Viewport: 1'].setValues(displayedObject=p1)
    mdb.models['Model-1'].parts['LowerPlate'].features.changeKey(fromName='RP', 
        toName='Lower')
    p1 = mdb.models['Model-1'].parts['UpperPlate']
    session.viewports['Viewport: 1'].setValues(displayedObject=p1)
    mdb.models['Model-1'].parts['UpperPlate'].features.changeKey(fromName='RP', 
        toName='Upper')
    p = mdb.models['Model-1'].parts['UpperPlate']
    r = p.referencePoints
    refPoints=(r[9], )
    p.Set(referencePoints=refPoints, name='Set-1')
    p1 = mdb.models['Model-1'].parts['LowerPlate']
    session.viewports['Viewport: 1'].setValues(displayedObject=p1)
    p = mdb.models['Model-1'].parts['LowerPlate']
    r = p.referencePoints
    refPoints=(r[2], )
    p.Set(referencePoints=refPoints, name='Set-1')
    p = mdb.models['Model-1'].parts['Blank']
    c = p.cells
    cells = c.getSequenceFromMask(mask=('[#1 ]', ), )
    p.Set(cells=cells, name='Set-1')
    p = mdb.models['Model-1'].parts['Blank']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    session.viewports['Viewport: 1'].partDisplay.setValues(sectionAssignments=ON, 
        engineeringFeatures=ON)
    session.viewports['Viewport: 1'].partDisplay.geometryOptions.setValues(
        referenceRepresentation=OFF)
    mdb.models['Model-1'].Material(name='Aluminium')
    mdb.models['Model-1'].materials['Aluminium'].Density(table=((@materialDestiny, ), ))
    mdb.models['Model-1'].materials['Aluminium'].Elastic(table=((@YoungsModulus, @PoissonsRatio), ))
    mdb.models['Model-1'].materials['Aluminium'].Plastic(table=((@Plastic)))
    mdb.models['Model-1'].HomogeneousSolidSection(name='AluminiumBlank', 
        material='Aluminium', thickness=None)
    p = mdb.models['Model-1'].parts['Blank']
    c = p.cells
    cells = c.getSequenceFromMask(mask=('[#1 ]', ), )
    region = regionToolset.Region(cells=cells)
    p = mdb.models['Model-1'].parts['Blank']
    p.SectionAssignment(region=region, sectionName='AluminiumBlank', offset=0.0, 
        offsetType=MIDDLE_SURFACE, offsetField='', 
        thicknessAssignment=FROM_SECTION)
    p = mdb.models['Model-1'].parts['UpperPlate']
    session.viewports['Viewport: 1'].setValues(displayedObject=p)
    p = mdb.models['Model-1'].parts['UpperPlate']
    r = p.referencePoints
    refPoints=(r[9], )
    region=regionToolset.Region(referencePoints=refPoints)
    mdb.models['Model-1'].parts['UpperPlate'].engineeringFeatures.PointMassInertia(
        name='Mass', region=region, mass=0.275, alpha=0.0, composite=0.0)
    a = mdb.models['Model-1'].rootAssembly
    session.viewports['Viewport: 1'].setValues(displayedObject=a)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(
        optimizationTasks=OFF, geometricRestrictions=OFF, stopConditions=OFF)
    a = mdb.models['Model-1'].rootAssembly
    a.DatumCsysByDefault(CARTESIAN)
    p = mdb.models['Model-1'].parts['Blank']
    a.Instance(name='Blank-1', part=p, dependent=OFF)
    a = mdb.models['Model-1'].rootAssembly
    p = mdb.models['Model-1'].parts['LowerPlate']
    a.Instance(name='LowerPlate-1', part=p, dependent=OFF)
    a = mdb.models['Model-1'].rootAssembly
    a.translate(instanceList=('LowerPlate-1', ), vector=(0.0, 0.0, @blankHeight))
    a = mdb.models['Model-1'].rootAssembly
    p = mdb.models['Model-1'].parts['UpperPlate']
    a.Instance(name='UpperPlate-1', part=p, dependent=OFF)
    a = mdb.models['Model-1'].rootAssembly
    a.translate(instanceList=('UpperPlate-1', ), vector=(0.0, 0.0, -20.0))
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(
        adaptiveMeshConstraints=ON)
    mdb.models['Model-1'].ExplicitDynamicsStep(name='Crash', previous='Initial', 
        timePeriod=0.05)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(step='Crash')
    mdb.models['Model-1'].fieldOutputRequests['F-Output-1'].setValues(
        numIntervals=60)
    mdb.models['Model-1'].FieldOutputRequest(name='F-Output-2', 
        createStepName='Crash', variables=('EVOL', ))
    regionDef=mdb.models['Model-1'].rootAssembly.instances['UpperPlate-1'].sets['Set-1']
    mdb.models['Model-1'].FieldOutputRequest(name='F-Output-3', 
        createStepName='Crash', variables=('U', ), region=regionDef, 
        sectionPoints=DEFAULT, rebar=EXCLUDE)
    regionDef=mdb.models['Model-1'].rootAssembly.instances['Blank-1'].sets['Set-1']
    mdb.models['Model-1'].FieldOutputRequest(name='F-Output-4', 
        createStepName='Crash', variables=('UT', ), region=regionDef, 
        sectionPoints=DEFAULT, rebar=EXCLUDE)
    regionDef=mdb.models['Model-1'].rootAssembly.instances['LowerPlate-1'].sets['Set-1']
    mdb.models['Model-1'].HistoryOutputRequest(name='H-Output-2', 
        createStepName='Crash', variables=('RF1', 'RF2', 'RF3'), 
        region=regionDef, sectionPoints=DEFAULT, rebar=EXCLUDE)
    regionDef=mdb.models['Model-1'].rootAssembly.instances['UpperPlate-1'].sets['Set-1']
    mdb.models['Model-1'].HistoryOutputRequest(name='H-Output-3', 
        createStepName='Crash', variables=('RF1', 'RF2', 'RF3','U3'), 
        region=regionDef, sectionPoints=DEFAULT, rebar=EXCLUDE)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(interactions=ON, 
        constraints=ON, connectors=ON, engineeringFeatures=ON, 
        adaptiveMeshConstraints=OFF)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(step='Initial')
    mdb.models['Model-1'].ContactProperty('IntProp-1')
    mdb.models['Model-1'].interactionProperties['IntProp-1'].TangentialBehavior(
        formulation=PENALTY, directionality=ISOTROPIC, slipRateDependency=OFF, 
        pressureDependency=OFF, temperatureDependency=OFF, dependencies=0, 
        table=((0.2, ), ), shearStressLimit=None, maximumElasticSlip=FRACTION, 
        fraction=0.005, elasticSlipStiffness=None)
    mdb.models['Model-1'].interactionProperties['IntProp-1'].NormalBehavior(
        pressureOverclosure=HARD, allowSeparation=ON, 
        constraintEnforcementMethod=DEFAULT)
    mdb.models['Model-1'].ContactExp(name='Int-1', createStepName='Initial')
    mdb.models['Model-1'].interactions['Int-1'].includedPairs.setValuesInStep(
        stepName='Initial', useAllstar=ON)
    mdb.models['Model-1'].interactions['Int-1'].contactPropertyAssignments.appendInStep(
        stepName='Initial', assignments=((GLOBAL, SELF, 'IntProp-1'), ))
    session.viewports['Viewport: 1'].view.setValues(nearPlane=56.1923, 
        farPlane=102.169, width=50.0543, height=26.6016, cameraPosition=(
        74.444, -15.063, -25.3388), cameraUpVector=(-0.142451, 0.963961, 
        0.224695), cameraTarget=(2.76787, -5.51422, 4.04393))
    session.viewports['Viewport: 1'].view.setValues(nearPlane=60.1055, 
        farPlane=111.369, width=53.5401, height=28.4541, cameraPosition=(
        12.7052, -11.6257, 82.636), cameraUpVector=(0.861032, 0.258649, 
        -0.437864), cameraTarget=(1.88729, -5.46519, 5.58398))
    session.viewports['Viewport: 1'].view.setValues(nearPlane=61.2292, 
        farPlane=115.198, width=54.5411, height=28.9861, cameraPosition=(
        4.97509, -55.0485, 67.3837), cameraUpVector=(0.896725, 0.408653, 
        -0.16996), cameraTarget=(1.1943, -9.35798, 4.21663))
    a = mdb.models['Model-1'].rootAssembly
    s1 = a.instances['LowerPlate-1'].faces
    side2Faces1 = s1.getSequenceFromMask(mask=('[#1 ]', ), )
    region1=regionToolset.Region(side2Faces=side2Faces1)
    a = mdb.models['Model-1'].rootAssembly
    s1 = a.instances['Blank-1'].faces
    side1Faces1 = s1.getSequenceFromMask(mask=('[#2 ]', ), )
    region2=regionToolset.Region(side1Faces=side1Faces1)
    mdb.models['Model-1'].Tie(name='Constraint-1', master=region1, slave=region2, 
        positionToleranceMethod=COMPUTED, adjust=ON, tieRotations=ON, 
        thickness=ON)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(loads=ON, bcs=ON, 
        predefinedFields=ON, interactions=OFF, constraints=OFF, 
        engineeringFeatures=OFF)
    a = mdb.models['Model-1'].rootAssembly
    r1 = a.instances['LowerPlate-1'].referencePoints
    refPoints1=(r1[2], )
    region = regionToolset.Region(referencePoints=refPoints1)
    mdb.models['Model-1'].EncastreBC(name='BC-1', createStepName='Initial', 
        region=region, localCsys=None)
    a = mdb.models['Model-1'].rootAssembly
    r1 = a.instances['UpperPlate-1'].referencePoints
    refPoints1=(r1[9], )
    region = regionToolset.Region(referencePoints=refPoints1)
    mdb.models['Model-1'].DisplacementBC(name='BC-2', createStepName='Initial', 
        region=region, u1=SET, u2=SET, u3=UNSET, ur1=SET, ur2=SET, ur3=SET, 
        amplitude=UNSET, distributionType=UNIFORM, fieldName='', 
        localCsys=None)
    a = mdb.models['Model-1'].rootAssembly
    r1 = a.instances['UpperPlate-1'].referencePoints
    refPoints1=(r1[9], )
    region = regionToolset.Region(referencePoints=refPoints1)
    #must be velocity 3 = 420, but changed to 400, because matrix doesn't close
    mdb.models['Model-1'].Velocity(name='Predefined Field-1', region=region, 
        field='', distributionType=MAGNITUDE, velocity1=0.0, velocity2=0.0, 
        velocity3=@blankHeight / 0.05, omega=0.0)
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
    partInstances =(a.instances['Blank-1'], )
    a.seedPartInstance(regions=partInstances, size=@Mesh, deviationFactor=0.1, 
        minSizeFactor=0.1)
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['UpperPlate-1'], )
    a.seedPartInstance(regions=partInstances, size=@Mesh, deviationFactor=0.1, 
        minSizeFactor=0.1)
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['LowerPlate-1'],  )
    a.seedPartInstance(regions=partInstances, size=10.5, deviationFactor=0.1, 
        minSizeFactor=0.1)
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['Blank-1'], )
    a.generateMesh(regions=partInstances)
    a = mdb.models['Model-1'].rootAssembly
    partInstances =(a.instances['LowerPlate-1'], a.instances['UpperPlate-1'], )
    a.generateMesh(regions=partInstances)
    session.viewports['Viewport: 1'].assemblyDisplay.setValues(mesh=OFF)
    session.viewports['Viewport: 1'].assemblyDisplay.meshOptions.setValues(
        meshTechnique=OFF)
    mdb.Job(name='@Name', model='Model-1', description='', type=ANALYSIS, 
        atTime=None, waitMinutes=0, waitHours=0, queue=None, 
        explicitPrecision=SINGLE, nodalOutputPrecision=SINGLE, echoPrint=OFF, 
        modelPrint=OFF, contactPrint=OFF, historyPrint=OFF, userSubroutine='', 
        scratch='', parallelizationMethodExplicit=DOMAIN, numDomains=@Core, 
        activateLoadBalancing=False, multiprocessingMode=DEFAULT, numCpus=@Core)
    mdb.jobs['@Name'].submit(consistencyChecking=OFF)
    
Macro1()

