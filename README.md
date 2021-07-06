# Pixel perfect screen sharing in the Web browser using ASP .NET SignalR

This project was created for [Burgas Free University](https://www.bfu.bg/en).   
![Burgas Free University Logo](https://www.bfu.bg/assets/images/logo_en.jpg)


Here is a preview of the project:

https://user-images.githubusercontent.com/20871961/124635934-aaf87a80-de90-11eb-9e52-4b8f0a7a034e.mp4

The idea is that we have one `master` project that can share it's screen. The screensharing is established via .NET SignalR websockets. The `slave` are the user/s that will watch the `master` screen.  

On the video we can see the `master` on the left side and the `slave` on the right side. One `master` can have many `slaves` or users that will observe his screen. The `slave` can only watch and can't resize, scroll or modify any input elements. If the `master` resizes, scrolls or changes any input elements, the `slaves` screen is dynamically changed.
