# AvaloniaInside.MonoGame + Dock (WIP ) , MVP demo..

Integration of MonoGame for Avalonia  (DOCK BRANCH + Avalonia.Inside + working samples)  thanks to  wieslawsoltes/AvaloniaInside.MonoGame

## Install:  clone this fork or the  wieslawsoltes/AvaloniaInside.MonoGame    ( DockDemo branch) .. I will keep it  in sync with upstream wieslawsoltes/AvaloniaInside.MonoGame and maybe try some expirements in here and mabye add some  2d shader  tests , scripting,  such.

set  AvaloniaInside.MonoGameExample.Desktop as default project , this one works the others do not run or crash  , there is a wasm version but its doensnt seem to work yet.. Avalonia is getting an update soon.    ( build and run debug or release) its a WIP  but you can add inspectors , grips , editing stuff from Core2D, or other avalonia repos and samples

tested on windows, bild and tested on M1 apple osx.

any contributiongs MG specify like shaders are welcome.

Sponsor wieslawsoltes and or Avalonia I can't promise  to maintain this vault 


it uses Reactive and MVVM but with the samples you can figure it out comping from  WPF

Maybe we can make a level edtitor , or  IDE like shader toy wiht scripting with it later.

Tested

tab activation, layout save  and load,  resize, swtich tabs, one MG view  works

known issues .. control is destroyed recreated on switch tab as did earlier wpf ones, which is oke.., if undocked  and big it might have glitches.


>>>NON Dcoking  instructions
## Install

Install the package to your project using command below or open the [package nuget](https://www.nuget.org/packages/AvaloniaInside.MonoGame) to find other way to install.

```bash
dotnet add package AvaloniaInside.MonoGame
```

## Usage

Add the namespace to your view 
```
xmlns:monoGame="clr-namespace:AvaloniaInside.MonoGame;assembly=AvaloniaInside.MonoGame"
```

Add the game control. At the example below we use `CurrentGame` property to the game.
```xml
<monoGame:MonoGameControl Game="{Binding CurrentGame}" />
```

```csharp
public Game CurrentGame { get; set; } = new MyExampleGame();
```

![image](https://user-images.githubusercontent.com/956077/211166326-10a244a2-f265-4846-947a-6991133ce25a.png)


## Known issues
1. Mobile not working.
2. There is no implementation of device input, This is could be manage by native Avalonia.      Or mabye polling the MGInput .  Feel tree to add PR for this..
3. Not a good performance for the moment.
