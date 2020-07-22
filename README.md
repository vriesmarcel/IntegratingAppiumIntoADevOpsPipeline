# Integrating Appium Into a DevOps Pipeline

Companion repo for my Pluralsight course Getting "Integrating Appium Into a DevOps Pipeline"

this repo demonstrates how to integrate appium UI tests into GitHub actions. The workflow now incorporates the build of a Visual Studio 2019 solution that contains a iOS, Android, UWP, WPF and WIndows forms app and the UI tests for all these different platforms. The workfow will build all apss and only test the UWP and Android version of the app as part of the test step.

The run requires a custom runner that has the required tools installed (like a "normal" developer box) and then will use a preconfigured Android emulator. 

The moment the course is published I will share the link here