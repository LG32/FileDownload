﻿using System;
using System.Collections.Generic;
using System.IO;

namespace FileDownloader.Model
{
    [Serializable]
    public class DownloadInfo
    {
        #region 字段

        private string _fileUrl;
        private long _downloadSize;
        private string _dicPath;
        private long _fileSize;
        private string _fileName;
        private bool _isComplate;
        private List<FileBlock> _fileBlocks = new List<FileBlock>();
        #endregion

        #region 属性
        public string FileUrl
        {
            get => _fileUrl;
            set => _fileUrl = value;
        }
        public bool IsComplate
        {
            get => DownloadSize >= FileSize;
            set => _isComplate = value;
        }
        public long DownloadSize
        {
            get
            {
                _downloadSize = 0;
                foreach (var fileBlock in FileBlocks)
                {
                    _downloadSize += fileBlock.DownloadSize;
                }

                return _downloadSize;
            }
            // set => _downloadSize = value;
        }
        /// <summary>
        /// 下载信息保存位置
        /// </summary>
        public string SavePath
        {
            get => Path.Combine(DicPath, FileName);
        }
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string DicPath
        {
            get => _dicPath;
            set => _dicPath = value;
        }
        public long FileSize
        {
            get => _fileSize;
            set => _fileSize = value;
        }
        public string FileName
        {
            get => _fileName;
            set => _fileName = value;
        }
        public int ThreadNum
        {
            get
            {
                return 1;
            }
            // set => _threadNum = value;
        }

        public List<FileBlock> FileBlocks
        {
            get => _fileBlocks;
            set => _fileBlocks = value;
        }
        #endregion

        public DownloadInfo()
        {

        }
        public DownloadInfo(string fileUrl, long fileSize, string fileName, string dicPath)
        {
            this._fileUrl = fileUrl;
            this._fileSize = fileSize;
            this._fileName = fileName;
            this._dicPath = dicPath;
        }
    }
}
