param($installPath, $toolsPath, $package, $project)

$Leap_Item0 = $project.ProjectItems.Item("Leap.dll")
$Leap_Item0.Delete()

$LeapCSharp_Item0 = $project.ProjectItems.Item("LeapCSharp.dll")
$LeapCSharp_Item0.Delete()
