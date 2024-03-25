﻿using System.Diagnostics;
using System.IO;
using WPFMVVM.ViewModels.Base;

namespace WPFMVVM.ViewModels
{
    class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;
        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                        .EnumerateDirectories()
                        .Select(dir => new DirectoryViewModel(dir.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e);
                }
                return Enumerable.Empty<DirectoryViewModel>();
            }
        }
        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                        .EnumerateDirectories()
                        .Select(file => new FileViewModel(file.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e);
                }
                return Enumerable.Empty<FileViewModel>();
            }
        }
        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDirectories.Cast<object>().Concat(Files.Cast<object>());
                }
                catch (UnauthorizedAccessException e) 
                { 
                    Debug.WriteLine(e);
                }
                return Enumerable.Empty<object>();
            }
        }
        public string Name => _DirectoryInfo.Name;
        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;
        public DirectoryViewModel(string path) => _DirectoryInfo = new DirectoryInfo(path);

    }

    class FileViewModel : ViewModel
    {
        private FileInfo _FileInfo;
        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        public DateTime CreationTime => _FileInfo.CreationTime;
        public FileViewModel(string path) => _FileInfo = new FileInfo(path);
    }
}
