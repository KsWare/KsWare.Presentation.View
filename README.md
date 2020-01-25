# KsWare.Presentation.ViewFramework.Common
Common classes for KsWare Presentation Framework for View

- [VisibilityBinding](##VisibilityBinding)
- [RootBindingExtension](##RootBindingExtension)
- [RootElementExtension](##RootElementExtension)
- [SharedWidth (attached behavior)](##SharedWidth)
- [Design.Properties](##Design.Properties)
- [BindingProxy](##BindingProxy)


## VisibilityBinding
## RootBindingExtension
## RootElementExtension
## SharedWidth
Provides support for a shared width (attached property)

example: Both Labels will have same width (largest width of the group).
```xml
xmlns:ab="clr-namespace:KsWare.Presentation.ViewFramework.AttachedBehavior;assembly=KsWare.Presentation" 
<Label Content="longest text" ab:SharedWidth.Group="swGroupA"/>
<Label Content="short" ab:SharedWidth.Group="swGroupA"/>
```
## Design.Properties
Provides property values for design time.
```xml
<TextBox Design.Background="Aqua" Design.Visibility="Hidden">
    <Design.Properties>
        <DesignProperty Name="Text" Value="Visible" />
        <DesignProperty Name="BorderBrush" Value="#FFFF0000" />
        <DesignProperty Name="Width" Value="10cm" />
        <DesignProperty Name="BorderThickness" Value="1,2,3,4" />
    </Design.Properties>
</TextBox>
```
## BindingProxy

```xaml
<UserControl.Resources>
   <BindingProxy x:Key="DataContextProxy" Value="{Binding}"/>
</UserControl.Resources>
```
```xaml
<Button 
    Command="{Binding Source={StaticResource DataContextProxy}, Path=Value.DoAnythingCommand}"
/>
```

| |Master|Develop|Kux|
|---|---|---|---|
|Build|[![Build status](https://ci.appveyor.com/api/projects/status/f6egmwg7elfxua7y/branch/master?svg=true)](https://ci.appveyor.com/project/KsWare/KsWare-Presentation-ViewFramework-Common/branch/master)|[![Build status](https://ci.appveyor.com/api/projects/status/f6egmwg7elfxua7y/branch/develop?svg=true)](https://ci.appveyor.com/project/KsWare/KsWare-Presentation-ViewFramework-Common/branch/develop)|[![Build status](https://ci.appveyor.com/api/projects/status/f6egmwg7elfxua7y/branch/develop?svg=true)](https://ci.appveyor.com/project/KsWare/KsWare-Presentation-ViewFramework-Common/branch/features/kux)|
|Test|![AppVeyor tests (branch)](https://img.shields.io/appveyor/tests/ksware/KsWare-Presentation-ViewFramework-Common/master)|![AppVeyor tests (branch)](https://img.shields.io/appveyor/tests/ksware/KsWare-Presentation-ViewFramework-Common/develop)|![AppVeyor tests (branch)](https://img.shields.io/appveyor/tests/ksware/KsWare-Presentation-ViewFramework-Common/features/kux)|
|Nuget||[![NuGet Badge](https://buildstats.info/nuget/KsWare.Presentation.ViewFramework.Common)](https://www.nuget.org/packages/KsWare.Presentation.ViewFramework.Common/)|
