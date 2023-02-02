using System;
using System.Collections.Generic;
using AvaloniaInside.MonoGameExample.ViewModels;
using Dock.Avalonia.Controls;
using Dock.Model.Avalonia;
using Dock.Model.Avalonia.Controls;
using Dock.Model.Core;

namespace AvaloniaInside.MonoGameExample.Views;

public class DockFactory : Factory
{
    private readonly MainViewModel _mainViewModel;

    public DockFactory(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public DocumentDock DocumentsPane { get; set;  }

    public override void InitLayout(IDockable layout)
    {
        ContextLocator = new Dictionary<string, Func<object?>>
        {
            ["Document1"] = () => _mainViewModel.Game1,
            ["Document2"] = () => _mainViewModel.Game2,
            ["Properties"] = () => _mainViewModel,
        };

        DockableLocator = new Dictionary<string, Func<IDockable?>>
        {
            //["Root"] = () => _rootDock,
            //["Files"] = () => _documentDock,
            //["Find"] = () => _findTool,
            //["Replace"] = () => _replaceTool
        };

        HostWindowLocator = new Dictionary<string, Func<IHostWindow?>>
        {
            [nameof(IDockWindow)] = () => new HostWindow()
        };

        if (layout is IDock dock)
        {
            var documentPane = FindDockable(dock, x => x.Id == "DocumentsPane");
            if (documentPane is DocumentDock documentDock)
            {
                DocumentsPane = documentDock;
            }
        }

        base.InitLayout(layout);
    }
}