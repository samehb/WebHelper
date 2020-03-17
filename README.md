# WebHelper
![WebHelper](https://i.imgur.com/TgsDmBy.png)

## Introduction
WebHelper is a security tool that allows you to view and modify programs web traffic. It works by utilizing a web proxy server (that the programs connect to).

## Features
1. Easy to use.
2. Supports both normal (HTTP) and encrypted (HTTPS) traffic.
3. Supports rules which allow you to block, forward, download, and modify (to perform Man-in-the-middle attacks) requests. GET and POST request parameters are supported for url matching. Regex patterns are also supported.
4. Supports auto capture on start.
5. Supports both local and remote (useful if you are using an external device) connections.

## Installation
1. Download the tool from [here](https://github.com/samehb/WebHelper/releases).
2. Extract it into any folder of choice.
3. Allow WebHelper.exe in your firewall.

## Usage
1. Run WebHelper.exe as **Administrator**.
2. Click "Start" to start the capture. You will see a Security Warning asking if you want to install a certificate. Click Yes. This certificate is needed to allow the proxy to decrypt HTTPS traffic. The tool changes the system's proxy settings to that of the internal proxy.
3. Most programs use the system's proxy settings. Though, there are some program that do not. For those programs, you need to copy the system's proxy setting into them, and enable the proxy option. You will also need to do this if you are using programs on external devices.
4. Launch the program that you want to monitor, and you will be able to see the HTTP(S) traffic on the tool's main window.
5. If you are using a browser program, it could complain that the connection is not safe, when you visit HTTPS sites. That is normal. All you need to do is trust the certificate, then refresh the page.
6. Some websites use HSTS (HTTP Strict Transport Security), and some browsers enforce it. You may not be able to trust the certificate on those sites. You have two choices to bypass the restriction. You can use a different browser, or you can find a way to disable HSTS by editing some browser files.
7. You can use a filter before starting the capture of traffic, by using the TextBox near the Start button.
8. You cannot edit the Settings until you stop the capture.

## Settings
* **Decrypt HTTPS** is enabled by default, and allows you to view HTTPS traffic. You may disable this, if you are not debugging HTTPS traffic.
* **Use Machine Store** allows you install the proxy certificate into the machine store instead of user store.
* **Hide Blocked Hosts Links** is enabled by default, and allows you to block hosts requests from being displayed on main window.
* **Auto Capture on Start** allows you to start the tool minimized while capturing traffic.
* **Allow Remote Connections** allows external devices (like cell phones) to connect to the proxy.
* **Parse Rules on Capture** allows you to parse/reload the rules on capture (pressing the Start button).

## Rules

You will find the following rules files inside the **rules** folder:

* **block.txt** <- Adding hosts into this file will result in the program/proxy blocking them.
* **download.txt** <- Adding links into this file will allow them to be handled for download.
* **downloadregex.txt** <- This is like download.txt, but it supports regex (only). Do not add regex rules into download.txt.
* **forward.txt** <- Adding links into this file will allow them to be forwarded to other links.
* **forwardregex.txt** <- This is like forward.txt, but it supports regex (only). Do not add regex rules into forward.txt.
* **respond.txt** <- Adding links into this file will allow you to modify their responses, and play the man in the middle.
* **respondregex.txt** <- This is like respond.txt, but it supports regex (only). Do not add regex rules into respond.txt.

Comments are allowed inside all rules files. Any line starting with a semicolon (**;**), is a comment.

```
;This is a comment.
;www.mywebsiteexample.com <- this is a comment / ignored rule
```

**block.txt** rule file contains one-part rules. Here is an example.

```
www.thesitethatidontwant.com
```

The rest of the rules files contain two-part rules. The first part is the link/url. The second part is the action.
For example, the following rule (on **forwards.txt**) will forward mywebsiteexample.com into myotherwebsite.com

```
mywebsiteexample.com;myotherwebsite.com
```

You can also create your own methods, for handling urls on rules files, into **WebHelperMethods.cs** (on the project).
The following example contains a method action. When the program matches www.mywebsiteexample.com, it will call the method 
associated with mytest, on WebHelperMethods.cs, to handle it.

```
www.mywebsiteexample.com;method=mytest
```

When it comes to rules, you can use exact link matching like:

```
mywebsiteexample.com;myotherwebsite.com
```

You can also use wildcards, which would allow you to match anything that starts with the url.
The following rule will match www.mywebsiteexample.com, www.mywebsiteexample.com/test, www.mywebsiteexample.com/test/ etc.

```
www.mywebsiteexample.com***;myotherwebsite.com
```


The following rule matches mywebsiteexample.com/test with the GET parameters test=test. Add "?" to specify GET parameters.

```
mywebsiteexample.com/test?test=test;myotherwebsite.com
```

The following rule matches mywebsiteexample.com/test with the POST parameters test=test. Add "||" to specify POST parameters.

```
mywebsiteexample.com/test||test=test;myotherwebsite.com
```

Feel free to check the rules files for more information and examples.

## Copyright
License: CC BY-NC-SA 4.0 (Attribution-NonCommercial-ShareAlike 4.0 International)

Read file [LICENSE](LICENSE)

## Links

[Blog](http://sres.tumblr.com)

[Discussion]()