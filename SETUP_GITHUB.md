# GitHub Setup Guide for UFetch

This guide will help you push your UFetch project to GitHub and make it installable via Unity Package Manager.

## 📋 Prerequisites

- Git installed on your system
- A GitHub account
- Unity project with UFetch package structure ready

## 🚀 Step 1: Initialize Git Repository

Open a terminal in your project root directory (`d:\Unity\UFetch`) and run:

```bash
git init
git add .
git commit -m "Initial commit: UFetch Unity Package"
```

## 📦 Step 2: Create GitHub Repository

1. Go to [GitHub](https://github.com) and sign in
2. Click the **+** icon in the top-right corner → **New repository**
3. Fill in the details:
   - **Repository name**: `UFetch` (or your preferred name)
   - **Description**: "Modern async/await HTTP client for Unity"
   - **Public** or **Private**: Choose based on your needs (Public for open source)
   - **DO NOT** initialize with README, .gitignore, or license (we already have these)
4. Click **Create repository**

## 🔗 Step 3: Connect and Push to GitHub

After creating the repository, GitHub will show you commands. Run these in your terminal:

```bash
git remote add origin https://github.com/herbou/UFetch.git
git branch -M main
git push -u origin main
```

## ⚙️ Step 4: Package Configuration

The package.json has already been configured:

```json
{
  "name": "com.herbou.ufetch",
  "author": {
    "name": "Hamza HERBOU",
    "url": "https://github.com/herbou"
  }
}
```

No additional changes needed!

## 📥 Step 5: Install in Other Unity Projects

Now you can install UFetch in any Unity project using one of these methods:

**Important:** UFetch requires Newtonsoft.Json. Install it first before installing UFetch.

### Prerequisites: Install Newtonsoft.Json

Choose one of these options:

**Option A: Unity's Official Package (Recommended)**
1. Package Manager → **+** → **Add package by name**
2. Enter: `com.unity.nuget.newtonsoft-json`

**Option B: jillejr's Package**
1. Package Manager → **+** → **Add package from git URL**
2. Enter: `https://github.com/jilleJr/Newtonsoft.Json-for-Unity.git#upm`

### Method 1: Via Package Manager UI

1. Open Unity Package Manager (Window → Package Manager)
2. Click the **+** button in the top-left
3. Select **Add package from git URL**
4. Enter:
   ```
   https://github.com/herbou/UFetch.git?path=/Assets/UFetch
   ```
5. Click **Add**

### Method 2: Via manifest.json

1. Open `Packages/manifest.json` in your Unity project
2. Add both packages to the `"dependencies"` section:
   ```json
   "com.unity.nuget.newtonsoft-json": "3.2.1",
   "com.herbou.ufetch": "https://github.com/herbou/UFetch.git?path=/Assets/UFetch"
   ```
3. Save the file and return to Unity (it will auto-refresh)

### Method 3: Install Specific Version (Using Tags)

To install a specific version, you can use Git tags:

1. First, create a release tag in your repository:
   ```bash
   git tag v1.0.0
   git push origin v1.0.0
   ```

2. Then install using the tag:
   ```
   https://github.com/herbou/UFetch.git?path=/Assets/UFetch#v1.0.0
   ```

## 🏷️ Step 6: Create Releases (Recommended)

It's good practice to create GitHub releases for version management:

1. Go to your repository on GitHub
2. Click **Releases** (right sidebar)
3. Click **Create a new release**
4. Fill in:
   - **Tag version**: `v1.0.0` (create new tag)
   - **Release title**: `UFetch v1.0.0`
   - **Description**: List features and changes
5. Click **Publish release**

## 📝 Package Structure Explanation

Your UFetch package now has this structure:

```
Assets/UFetch/
├── Runtime/                          # Runtime code (required)
│   ├── UFetch.cs                    # Main UFetch implementation
│   └── UFetch.Runtime.asmdef        # Assembly definition
├── Samples~/                        # Sample code (optional, tilde makes it ignored by Unity)
│   └── Demo/
│       └── Demo.cs                  # Example usage
├── package.json                     # Package manifest (required)
├── README.md                        # Package documentation
├── LICENSE.md                       # License file
└── UFetch_Documentation.md          # Full documentation
```

The `?path=/Assets/UFetch` in the git URL tells Unity to treat only the `Assets/UFetch` folder as the package root.

## 🔄 Updating the Package

When you make changes to UFetch:

1. Make your code changes
2. Update the version in `package.json`:
   ```json
   "version": "1.1.0"
   ```
3. Commit and push:
   ```bash
   git add .
   git commit -m "Update to v1.1.0: Add new features"
   git push
   ```
4. Create a new release tag:
   ```bash
   git tag v1.1.0
   git push origin v1.1.0
   ```
5. Create a GitHub release for the new version

## 🔧 Troubleshooting

### Issue: "No 'git' executable was found"
**Solution**: Install Git from [git-scm.com](https://git-scm.com/) and restart Unity

### Issue: "Package resolution error"
**Solution**:
- Ensure the path is correct: `?path=/Assets/UFetch`
- Check that your repository is public or you have access
- Verify `package.json` is valid JSON

### Issue: "Assembly reference errors"
**Solution**:
- Ensure Newtonsoft.Json is installed (it should auto-install)
- Check that `UFetch.Runtime.asmdef` references are correct

### Issue: Changes not showing up in Unity
**Solution**:
- In Package Manager, remove and re-add the package
- Or manually delete the package from `Library/PackageCache/` and refresh

## 📚 Additional Resources

- [Unity Package Manager Documentation](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
- [Creating Custom Packages](https://docs.unity3d.com/Manual/CustomPackages.html)
- [Git URL Format](https://docs.unity3d.com/Manual/upm-git.html)

## ✅ Verification Checklist

Before sharing your package, verify:

- [ ] Repository is pushed to GitHub
- [ ] `package.json` has correct name and author info
- [ ] README.md is clear and contains installation instructions
- [ ] License file is included
- [ ] Git URL works in a test Unity project
- [ ] Newtonsoft.Json dependency auto-installs
- [ ] Sample code (if included) works correctly
- [ ] Created at least one release tag (v1.0.0)

## 🎉 Success!

Your UFetch package is now available for installation via Unity Package Manager!

Share your Git URL with others:
```
https://github.com/herbou/UFetch.git?path=/Assets/UFetch
```

Or add installation instructions to your repository's README.
