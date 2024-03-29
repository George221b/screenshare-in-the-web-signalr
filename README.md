<div align="center"><h3 align="center">This project was created for <a href="https://www.bfu.bg/en">Burgas Free University</a> as a project defense for Bachelor degree in Software Engineering.</h3></div>
<p></p>

<p align="center">
  <img src="https://www.bfu.bg/assets/images/logo_en.jpg" alt="Burgas Free University Logo" />
</p>

## Project Description
Video preview of the project:

https://user-images.githubusercontent.com/20871961/124635934-aaf87a80-de90-11eb-9e52-4b8f0a7a034e.mp4

The idea is that we have one `master` project that can share its screen. The screensharing is established via .NET SignalR websockets. The `slave` are the user/s that will watch the `master` screen.  

On the video we can see the `master` on the left side and the `slave` on the right side. One `master` can have many `slaves` or users that will observe his screen. The `slave` can only watch and can't resize, scroll or modify any input elements. If the `master` resizes, scrolls or changes any input elements, the `slaves` screen is dynamically changed.

## Documentation
Detailed documentation (the diploma work) on Bulgarian can be found here: [BG Documentation](https://github.com/George221b/screenshare-in-the-web-signalr/blob/main/DiplomaWork.pdf). The last page is the page of contents. Chapter 3. follows the creation of the project step by step.

## License

Code by George Dimov. Copyright 2020 George Dimov.

This project is intended to be used in both open-source and commercial environments. You may choose to use screenshare-in-the-web-signalr  under either the Apache License, Version 2.0 (currently the only license in LICENSE file), or the MIT license. You are encouraged to evaluate both to determine which best fits your intended use.

Refer to the [LICENSE](https://github.com/George221b/screenshare-in-the-web-signalr/blob/main/LICENSE) for detailed information.

## Any questions, comments or additions?
If you have a feature request or bug report, leave an issue on the issues page or send a pull request.
