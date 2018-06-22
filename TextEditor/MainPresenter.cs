using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.BL;

namespace TextEditor
{
    class MainPresenter
    {
        private readonly IManeForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _messageService;
        private string _currentFilePatch;
        public MainPresenter(IManeForm view, IFileManager manager, IMessageService service)
        {
            _view = view;
            _manager = manager;
            _messageService = service;

            _view.SetSymbolCount(0);

            _view.ContentChanged += new EventHandler(_view_ContentChanged);
            _view.FileOpenClick += new EventHandler(_view_FileOpenClick);
            _view.FileSaveClick += new EventHandler(_view_FileSaveClick);
        }

        void _view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                string content = _view.Content;
                _manager.SaveContent(content, _currentFilePatch);
                _messageService.ShowMessage("Файл успешно сохранен!");
            }
            catch(Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
        void _view_FileOpenClick (object sender, EventArgs e)
        {
            try
            {
                string filePatch = _view.FilePatch;
                bool isExist = _manager.IsExist(filePatch);
                if (!isExist)
                {
                    _messageService.ShowExclamation("Выбранный файл не существует!");
                    return;
                }
                _currentFilePatch = filePatch;

                string content = _manager.GetContent(filePatch);
                int count = _manager.GetSymbolCount(content);

                _view.Content = content;
                _view.SetSymbolCount(count);
            }

            catch(Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
        void _view_ContentChanged(object sender, EventArgs e)
        {
            string content = _view.Content;
            int count = _manager.GetSymbolCount(content);
            _view.SetSymbolCount(count);
        }
    }
       
}
