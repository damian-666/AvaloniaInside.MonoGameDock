using System;
using System.Collections.Generic;
using AvaloniaInside.MonoGameExample.ViewModels;
using Dock.Avalonia.Controls;
using Dock.Model.Avalonia;
using Dock.Model.Avalonia.Controls;
using Dock.Model.Core;
using ReactiveUI;

namespace AvaloniaInside.MonoGameExample.Views;

public class DockFactory : Factory
{
    private readonly MainViewModel _mainViewModel;

    public DockFactory(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public DocumentDock? DocumentsPane { get; set;  }

    public override void InitLayout(IDockable layout)
    {
        ContextLocator = new Dictionary<string, Func<object?>>
        {
            ["Document1"] = () => _mainViewModel.Game1,
            ["Document2"] = () => _mainViewModel.Game2,
            ["Document3"] = () => _mainViewModel.Game3,
            ["Properties"] = () => _mainViewModel,
        };

        DockableLocator = new Dictionary<string, Func<IDockable?>>
        {
            ["DocumentsPane"] = () => DocumentsPane,
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
                
                DocumentsPane.CreateDocument = ReactiveCommand.Create(() => CreateDocumentFromTemplate());
            }
        }

        base.InitLayout(layout);
    }
    
    private object? CreateDocumentFromTemplate()
    {
        if (DocumentsPane is null)
        {
            return null;
        }
        if (DocumentsPane.DocumentTemplate is null || !DocumentsPane.CanCreateDocument)
        {
            return null;
        }

        _mainViewModel.Game3 = new TestGame1();
        _mainViewModel.CurrentGame = _mainViewModel.Game3;

        var document = new Document
        {
            Id = "Document3",
            Title = "Game3",
            Content = DocumentsPane.DocumentTemplate.Content
        };

        DocumentsPane.Factory?.AddDockable(DocumentsPane, document);
        DocumentsPane.Factory?.SetActiveDockable(document);
        DocumentsPane.Factory?.SetFocusedDockable(DocumentsPane, document);

        return document;
    }
}