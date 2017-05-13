# C# WindowsForms Custom UI and Controllers
---
By importing this DLL into your project, it allows you to easily customize your user interface within the designer and also use some of the custom controllers that come within that project.
##### Video:
[![YouTube Link](https://img.youtube.com/vi/ZyYGuy5Eb8w/0.jpg)](https://www.youtube.com/watch?v=ZyYGuy5Eb8w "YouTube Link")



## Description
---
The idea behind making this project was to separate the code that related to the design and the functionality of the form, inorder to avoid writting a lot of code in the application, in case you're ever going to need to do a borderless form that can be dragabble or resizeable.
It also comes with custom controllers listed below, and the list will be updated as soon as i create more custom controllers.
This will make it easier and save you time designing the user interface without the need to write a lot of code related to user interface.

## Custom Controllers:
---
* **4CheckBox** - Used for quiz applications where the user has to choose an option from the 4 boxes.
* **CheckBox** - Looks the same as 4CheckBox control, but as 1 CheckBox with custom design.
* **Button** - Great for applications where you need to click next or back, has some special properties for animated button or disabling button.
* **ControlBox** - Exit, Maxmize, Minimize buttons as 1 control. Great when you making a borderless form.
* **GradientPanel** - Panel that allows you to make the parent form draggable or set gradient color to it (In the video above, i show how i use it with the gradient form).

* **TitleBar** - Will be docked on top and allows you to drag the form and also comes with ControlBox in it and build in events. Double Click on it will maxmize the form.


## Quick Examples:
---
##### GiladButton

![GiladButton](/images/GiladButton_preview.gif?raw=true "GiladButton")

##### GiladButton Properties:

![Properties](/images/GiladButton_properties.gif?raw=true "Properties")




##### Importing DLL Controllers Into The WindowsForms Designer

![Importing DLL](/images/GiladButton_dll_import.gif?raw=true "Importing DLL")




## Contributing
---
You can always feel free to fork or make a pull request if you want to fill this repository with more controllers or 
modifying the existing controllers and I'll merge your pull request into the master branch.