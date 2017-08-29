# **WpfUtility**
---

[![NuGet](https://img.shields.io/nuget/v/MunkiWinchester.WpfUtility.svg?style=flat-square)](https://www.nuget.org/packages/MunkiWinchester.WpfUtility/)
[![Build Status](https://img.shields.io/appveyor/ci/MunkiWinchester/WpfUtility/master.svg?style=flat-square)](https://ci.appveyor.com/project/MunkiWinchester/wpfutility/branch/master)
[![License](https://img.shields.io/github/license/MunkiWinchester/WpfUtility.svg?style=flat-square)](https://github.com/MunkiWinchester/WpfUtility/blob/master/LICENSE)

---

**CircularLoadingAnimation**
==========
...

---
**ClipboardHelper**
==========
...

---
**HighlightTextBlock**
==========
...

---
**IntListHyperlinkTextBlock**
==========
...

---
**LoadingPanel**
==========
...

---
**NlogViewer**
==========

NlogViewer is a WPF-control to show logs written by/with Nlog

### **How to use?**

Reference the ```NlogViewer.dll``` in your project.

Add a namespace to your control/window, like this

```xml
    xmlns:logViewer="clr-namespace:WpfUtility.LogViewer;assembly=WpfUtility"
```

then add the control

```xml
    <logViewer:NlogViewer x:Name="LogCtrl" /> 
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
<logViewer:NlogViewer x:Name="LogCtrl" ItemSource="{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type UserControl}}, Path=DataContext.SelectedUserLogfileEntries}" />
```

The type of the collection has to be of
```csharp
ObservableCollection<NLog.LogEventInfo>
```

---
**NumberTextBox**
==========
...

---
**Paging**
==========
...

---
**UiServices**
==========
...

---
**CommandManagerHelper**
==========
...

---
**Converters**
==========
...

---
**DelegateCommand**
==========
...

---
**ObservableObject**
==========
...

---
**RelayCommand**
==========
...

---