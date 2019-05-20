# SmartThreadPool

SmartThreadPool is a thread pool written in C#. This repository written again with .NET Standard referenced by [SmartThreadPool](https://github.com/amibar/SmartThreadPool) repository. PerformanceCounter implementation removed from project. 

# Basic Usage

This is a Thread Pool; if you got here, you probably know what you need. If you want to understand the features and know how it works, keep reading the sections below. If you just want to use it, here is a quick usage snippet. See examples for advanced usage.

    // Create an instance of the Smart Thread Pool
    SmartThreadPool smartThreadPool = new SmartThreadPool();
    
    // Queue an action (Fire and forget)
    smartThreadPool.QueueWorkItem(System.IO.File.Copy,
    
    @"C:\Temp\myfile.bin", @"C:\Temp\myfile.bak");
    // The action (file copy) will be done in the background by the Thread Pool

# Roadmap
- .NET Standard doesn't support `Thread.Abort` method anymore. ThreadPool must be support CancellationToken. Some tests fail because of this reason and unable to stop started thread.