# **WpfUtility**
---
**NlogViewer**
==========

NlogViewer is a WPF-control to show logs written by/with Nlog

### **How to use?**

Reference the ```NlogViewer.dll``` in your project.

Add a namespace to your control/window, like this

```xml
xmlns:nlog ="clr-namespace:NlogViewer;assembly=NlogViewer"
```

then add the control

```xml
    <nlog:NlogViewer x:Name="logCtrl" /> 
```

To setup NlogViewer as a target, add the following to your Nlog.config.

```xml
  <extensions>
    <add assembly="WpfUtility" />
  </extensions>
  <targets>
    <target xsi:type="NlogViewer" name="ctrl" />
  </targets>
  <rules>
    <logger name="*" minlevel="Trace" writeTo="ctrl" />
  </rules>
```

If you want to show an *"old"* log file you can bind it via ItemSource to it.

**Important:** You have to set the DataContext to the control/window or else it is referring to itself.

Eg.:
```xml
<nlog:NlogViewer x:Name="LogCtrl" ItemSource="{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type UserControl}}, Path=DataContext.SelectedUserLogfileEntries}" />
```

The type of the collection has to be of
```csharp
ObservableCollection<NLog.LogEventInfo>
```
---