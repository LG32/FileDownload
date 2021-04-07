# FileDownload
缩水版c# pc端下载器  
参考的原项目：https://github.com/AndrewJEON/FileDownloader  
目的：在1m以内实现简单的下载功能（现在将图片资源去除后为100+k）  
具有断点续传的功能  
原先参考项目中的似乎有多线程下载的功能，但是使用上问题，时间不够懒得研究  
先屏蔽，改成了单线程下载，性能貌似下降了40%。  
![avatar](readme.png)