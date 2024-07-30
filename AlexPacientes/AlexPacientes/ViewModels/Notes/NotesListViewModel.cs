using AlexPacientes.Models.AppModels.Notes;
using AlexPacientes.Settings;
using AlexPacientes.Views.Notes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Notes
{
    class NotesListViewModel : ViewModelBase
    {
        private ObservableCollection<NoteModel> _notes;
        public ObservableCollection<NoteModel> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }
        private NoteModel _newNote;
        public NoteModel NewNote
        {
            get { return _newNote; }
            set { _newNote = value;
                OnPropertyChanged();
            }
        }
        private NoteModel _selectedNote;
        public NoteModel SelectedNote
        {
            get { return _selectedNote; }
            set { _selectedNote = value;
                OnPropertyChanged();
            }
        }

        public Command NoteSelectedCommand { get; set; }
        public Command AddNoteCommand { get; set; }
        public Command AddNoteCompletedCommand { get; set; }
        public NotesListViewModel(Page context) : base(context)
        {
            Notes = new ObservableCollection<NoteModel>();
            AddNoteCommand = new Command(() => OnAddNote());
            AddNoteCompletedCommand = new Command(() => OnAddNoteCompleted());
            NoteSelectedCommand = new Command((param) => OnNoteSelected(param as NoteModel));
            for (int x = 0; x < 2; x++)
            {
                Notes.Add(new NoteModel()
                {
                    Title = TestSources.GetRandomName(),
                    Content = TestSources.GetLoremMessage()
                });
            }
        }

        async void OnNoteSelected(NoteModel note)
        {
            this.SelectedNote = note;
            var page = new SelectNoteView();
            page.BindingContext = this;
            await Navigation.PushAsync(page);
        }

        async void OnAddNote()
        {
            NewNote = new NoteModel();
            var page = new AddNoteView();
            page.BindingContext = this;
            await Navigation.PushAsync(page);
        }
        async void OnAddNoteCompleted()
        {
            Notes.Add(NewNote);
            await Navigation.PopAsync();
        }

    }
}
