# Template Unity Project

## Git and Git LFS

Git LFS is used in this repo. Git LFS is a Git extension for storing large files. If you are new to Git LFS, it is worth having a quick read of [this guide](https://www.atlassian.com/git/tutorials/git-lfs).

**Each machine** that makes commits to this repo should install Git LFS.  
On macOS, Git LFS can be installed with `brew install git-lfs`. For other installation platforms see [here](https://git-lfs.github.com).  

This Unity project has been configured to work with Git. For info on how this is done, see [here](https://robots.thoughtbot.com/how-to-git-with-unity).  
In particular, it notes how merge conflicts can be resolved with the [Unity Smart Merge tool](https://docs.unity3d.com/Manual/SmartMerge.html).

Unity stores extra information in `.meta` files. *Some* of these `.meta` files should be ignored. In order to prevent accidentally commiting files th is also recommended that you put [this script](https://github.com/kayy/git-pre-commit-hook-unity-assets/blob/master/pre-commit) in `.git/hooks/` of the project (this is local to your machine). More info [here](https://github.com/kayy/git-pre-commit-hook-unity-assets).


## Tooling

This Unity project is set up to be used with Visual Studio. Visual Studio for macOS is available [here](https://www.visualstudio.com/vs/visual-studio-mac/).
It can also be used with [Visual Studio Code](https://code.visualstudio.com) or [JetBrains Rider](https://www.jetbrains.com/rider/).
