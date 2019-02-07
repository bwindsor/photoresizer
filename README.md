# photoresizer
Program for resizing images and videos.
All common image input and output formats are supported. At present video is limited to the free encoding functionality provided by Microsoft Expression Encoder, meaning output is only in WMV.

## Installation
Microsoft .NET framework v4.0 or above is required to run the program. Almost all modern computers will have this already installed.

1. Download and install [Microsoft Expression Encoder](https://www.microsoft.com/en-gb/download/details.aspx?id=18974). This is only required to process videos.
2. [Download the latest version of the software](https://github.com/bwindsor/photoresizer/releases/latest) and pick the zip file called vX.X.zip)
3. Extract the zip file somewhere on your computer.
4. Run `PhotoResizer.exe`. Your computer may ask whether you want to run the program (as it may detect it as a potential risk).  Select "Run anyway" if you wish to use the program and you trust that I haven't written a virus.

## Development
### Dependencies
* For the video part both for coding and for running the program, [Microsoft Expression Encoder](https://www.microsoft.com/en-gb/download/details.aspx?id=18974) is required to be installed. 
* This provides the references `Microsoft.Expression.Encoder` and `Microsoft.Expression.Encoder.Types`
### Creating a release
1. Build the solution in Release (x86) mode using Visual Studio 2017
2. Tag your release using `git tag -a v1.0 -m "Release change summary"`
3. In github, go to releases, click "draft a new release" and select the tag you just created.
4. Create a zip file of the release and name it `v1.0.zip` (replace `1.0` with the release version) - binaries are in the `bin/x86/release` folder where your repository is created. You should not include the `.pdb` files. This should mean 5 files are included in the release.
5. Attach the zip file of binaries to the release
6. Click Create Release

## Using the program
* To add photos and videos to be processed, just drag them into the box.
* You can drag either files or folders - for a folder any supported file types in all levels of subfolder will also be included.
* Any videos which you don't want to trim down can just be dragged along with the photos.
* Any videos which need trimming must be dragged one at a time into the trimming box on in the top right hand part of the window. You will then be able to select the start and end points of trimming.
* If you drag more than one file at the same time into this box only the first one will be used and the rest ignored. 
* Any files which you add which have already been added will be ignored.
* Any files which you add with unsupported file extensions will be ignored.
* **Output files will be placed in a new folder called `Resized` alongside their original location.** For example the file `C:\Pics\MyPic.jpg` would be output to `C:\Pics\Resized\MyPic.jpg`.
