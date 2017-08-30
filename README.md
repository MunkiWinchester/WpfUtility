# **WpfUtility**

[![NuGet](https://img.shields.io/nuget/v/MunkiWinchester.WpfUtility.svg?style=flat-square)](https://www.nuget.org/packages/MunkiWinchester.WpfUtility/)
[![Build Status](https://img.shields.io/appveyor/ci/MunkiWinchester/WpfUtility/master.svg?style=flat-square)](https://ci.appveyor.com/project/MunkiWinchester/wpfutility/branch/master)
[![License](https://img.shields.io/github/license/MunkiWinchester/WpfUtility.svg?style=flat-square)](https://github.com/MunkiWinchester/WpfUtility/blob/master/LICENSE)

---

## List of content
* [Controls](#controls)
  * [CircularLoadingAnimation](#circularloadinganimation)
  * [HighlightTextBlock](#highlighttextblock)
  * [IntListHyperlinkTextBlock](#intlisthyperlinktextblock)
  * [LoadingPanel](#loadingpanel)
  * [NlogViewer](#nlogviewer)
  * [NumberTextBox](#numbertextbox)
  * [Paging](#paging)
* [Services](#services)
  * [ClipboardHelper](#clipboardhelper)
  * [CommandManagerHelper](#commandmanagerhelper)
  * [Converters](#converters)
  * [DelegateCommand](#delegatecommand)
  * [ObservableObject](#observableobject)
  * [RelayCommand](#relaycommand)
  * [UiServices](#uiservices)

---
# Controls

Add a namespace to your control/window, like this

```xml
        xmlns:guc="clr-namespace:WpfUtility.GeneralUserControls;assembly=WpfUtility"
```

One special case is the [NlogViewer](#nlogviewer), but how to use it is descriped.

## CircularLoadingAnimation
CircularLoadingAnimation is spinning loading animation.

![image](/Pictures/CircularLoadingAnimation.png)

### How to use?

To display the loading animation you have to set *IsLoading* to true otherwise it will be collapsed.

You can change the color of the animation to your liking simply by setting the *ForegroundColor*.

Eg.:
```xml
        <guc:CircularLoadingAnimation ForegroundColor="{Binding CircleColor}"
                                      IsLoading="{Binding IsLoading}" />
```

---
## HighlightTextBlock
HighlightTextBlock can highlight particullar parts of a text.

![image](/Pictures/HighlightTextBlock.png)

### How to use?

You mostly use it like a normal TextBlock, bind your *Text* and optional *Foreground* & *ToolTip*.
To highlight particullar parts in the text you have to bind it to the *HighlightPhrase*. Furthermore you can change if it is case sensitive, the background (*HighlightBrush*) and the foreground (*HighlightForeGround*) of the highlighted text.

Eg.:
```xml
        <guc:HighlightTextBlock Text="{Binding Track}"
                                HighlightPhrase="{Binding ElementName=TextBoxSearch, Path=Text}"
                                IsCaseSensitive="{Binding ElementName=CheckBox, Path=IsChecked}"
                                HighlightBrush="SandyBrown"
                                HighlightForeGround="Black"
                                ToolTip="{Binding Track}"
                                Foreground="Black" />
```
---
## IntListHyperlinkTextBlock
IntListHyperlinkTextBlock is a TextBlock to display a list with integers as one hyperlink per entry in the list.

![image](/Pictures/IntListHyperlinkTextBlock.png)

### How to use?

You can use it like a normal TextBlock but simply bind a list with integers to its *ItemSource* and configurate the *HyperlinkClickedEvent*.

The event will give you the clicked integer and you can trigger your actions.

The type of the collection for the ItemSource has to be of ```List<int>```.

Eg.:
```xml
        <guc:IntListHyperlinkTextBlock ItemSource="{Binding IntegerListe}"
                                       HyperlinkClickedEvent="ListHyperlinkTextBlock_OnHyperlinkClicked"
```

---
## LoadingPanel
LoadingPanel uses the [CircularLoadingAnimation](#circularloadinganimation) and can display two messages besides it.

![image](/Pictures/LoadingPanel.png)

### How to use?

As mentioned it uses the [CircularLoadingAnimation](#circularloadinganimation) and therefore the displaying and coloring are the same.

You can additionally bind a message, a submessage and the foregorund color for them to the control.

Eg.:
```xml
        <guc:LoadingPanel IsLoading="{Binding IsLoading}"
                          ForegroundColor="{Binding CircleColor}"
                          Message="{Binding MainMessage}"
                          MessageForegroundColor="{Binding MessageForegroundColor}"
                          SubMessage="{Binding SubMessage}" 
                          SubMessageForegroundColor="{Binding SubMessageForegroundColor}" />
```

---
## NlogViewer
NlogViewer shows logs written by/with Nlog.

![image](/Pictures/NlogViewer.png)

### How to use?

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

To not auto update the NlogViewer with newly written logfile entries you have to set *ActivateLoggers* to false. Per default the NlogViewer will display newly written logfile entries.

If you want to show an *"old"* log file you can bind it via ItemSource to it, but than you may want to deactivate the Loggers (*ActivateLoggers="false"*).

The type of the collection for the ItemSource has to be of ```ObservableCollection<NLog.LogEventInfo>```.

**Important:** You have to set the DataContext to the control/window or else it is referring to itself.

Eg.:
```xml
        <logViewer:NlogViewer ItemSource="{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type UserControl}}, Path=DataContext.SelectedUserLogfileEntries}"
                              ActivateLoggers="{Binding ElementName=CheckBoxActivateLoggers, Path=IsChecked}" />
```

---
## NumberTextBox
NumberTextBox is a TextBox which only allows numbers, the keys *delete*, *enter* and *back* are untouched and still functional.

---
## Paging
Paging is a control to display a paging navigator.

![image](/Pictures/Paging.png)

### How to use?
You bind your values (such as total entries) to the control and also your command to the "page change" commands.

If one of the "page change" command is triggered you can take your actions to load the next set of data from the database or something like that.

Eg.:
```xml
        <guc:Paging TotalEntries="{Binding TotalEntries}"
                    CurrentPage="{Binding CurrentPage}"
                    TotalPages="{Binding TotalPages}"
                    EntriesPerPage="{Binding EntriesPerPage}"
                    GoToFirstPageCommand="{Binding GoToFirstPageCommand}"
                    GoToPreviousPageCommand="{Binding GoToPreviousPageCommand}"
                    GoToNextPageCommand="{Binding GoToNextPageCommand}"
                    GoToLastPageCommand="{Binding GoToLastPageCommand}" />
```

---
# "Services"

## ClipboardHelper
...

---
## CommandManagerHelper
...

---
## Converters
...

---
## DelegateCommand
...

---
## ObservableObject
...

---
## RelayCommand
...

---
## UiServices
...
