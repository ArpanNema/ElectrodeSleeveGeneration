param($installPath, $toolsPath, $package, $project)

$Leap_Item0 = $project.ProjectItems.Item("Leap.dll")
$Leap_Item0.Delete()
$Leap_Path = $installPath + "\content\Leap.dll"
$Leap_Item = $project.ProjectItems.AddFromFile($Leap_Path)
$Leap_OutputProp = $Leap_Item.Properties.Item("CopyToOutputDirectory")
$Leap_OutputProp.Value = 2

$LeapCSharp_Item0 = $project.ProjectItems.Item("LeapCSharp.dll")
$LeapCSharp_Item0.Delete()
$LeapCSharp_Path = $installPath + "\content\LeapCSharp.dll"
$LeapCSharp_Item = $project.ProjectItems.AddFromFile($LeapCSharp_Path)
$LeapCSharp_OutputProp = $LeapCSharp_Item.Properties.Item("CopyToOutputDirectory")
$LeapCSharp_OutputProp.Value = 2
