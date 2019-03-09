# ARIoT2019
The contribution of private constructors from ARIoT 2019

## Getting started
This is the baseline for getting the project up and running. Some of the projects have separate readmes for additional details.

### Requirements
#### Software
* .NET Core 2.2: https://dotnet.microsoft.com/download
* Arduino Studio: https://www.arduino.cc/en/Main/Software
* Raspbian: https://www.raspberrypi.org/downloads/raspbian/
* Android Studio: https://developer.android.com/studio

#### Hardware
* LEGO Technic Bucket Wheel Excavator - 42055
* 3x Arduino Uno
* 2x Wemos D1 R2 Mini
* 2x RaspberryPi running Raspbian with cameras
* 2x LEGO NTX motors
* USB hub
* 5V 18amp PSU
* 16x32 LED display
Assemble with great care and compassion

### Installing

#### ExcavatorCore
See separate readme file for instructions

#### AutoExcavator
Build and run the project using Android Studio. Please update endpoint URLs to your own. Install on device according to the current Android practices. 

#### dataset
This folder contains labeled images for training your excavator to aim for black and yellow cups and avoid killing plants. We used CustomVision (https://www.customvision.ai/) for training our Excavator, but you don't have to. Make sure to train it though. We like plants.

#### electronics
Projects containing .ino files are updloaded using Arduino tools. DiggerSoft is installed on the RaspberryPis using command line tools.

## Project practices
### Code style
Fast and reckless

## Contributing
The core team has a strong belief distributed source control, and wish that contributions are isolated to separate forks. We ask that this is respected.

## License
This project is under the GNU General Public License V3 and is rated R for violence.

## Acknoledgements
A big thanks to the organizers of ARIoT for creating this awesome event!
