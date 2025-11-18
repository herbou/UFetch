# UFetch Package Structure

This document describes the final structure of the UFetch Unity Package Manager (UPM) package.

## 📦 Package Files (Assets/UFetch/)

This is the folder that will be installed when users add UFetch via Package Manager.

```
Assets/UFetch/
├── Runtime/                              # Runtime code folder
│   ├── UFetch.cs                        # Main HTTP client implementation (155 lines)
│   ├── UFetch.cs.meta                   # Unity metadata
│   └── UFetch.Runtime.asmdef            # Assembly definition with Newtonsoft.Json reference
│
├── Samples~/                            # Sample code (~ makes Unity ignore it)
│   └── Demo/
│       └── Demo.cs                      # Demo script showing UFetch usage
│
├── package.json                         # UPM package manifest (REQUIRED)
├── README.md                            # Package documentation
├── LICENSE.md                           # MIT License
└── UFetch_Documentation.md              # Complete API reference (342 lines)
```

## 📋 Key Files Explained

### package.json
The UPM package manifest that defines:
- Package name: `com.yourname.ufetch`
- Version: `1.0.0`
- Unity minimum version: `2021.3`
- Dependencies: Newtonsoft.Json (auto-installed)
- Samples reference

### Runtime/UFetch.cs
The main implementation file containing:
- `UFetch` static class with HTTP methods
- `UFetch.Options` configuration class
- `UFetch.UFetchResponse` response object

### Runtime/UFetch.Runtime.asmdef
Assembly definition that:
- Creates a separate assembly for UFetch
- References `Unity.Nuget.Newtonsoft-Json`
- Allows other packages to reference UFetch

### Samples~/Demo/
Sample code that users can import via Package Manager:
- Shows example usage patterns
- Not included in the package by default
- Users can import via Package Manager UI

## 🚀 Git URL Structure

When installing from GitHub, users will use:

```
https://github.com/yourusername/UFetch.git?path=/Assets/UFetch
```

The `?path=/Assets/UFetch` parameter tells Unity to treat only the `Assets/UFetch` folder as the package root, not the entire repository.

## 📁 Repository Structure

```
d:/Unity/UFetch/                         # Git repository root
├── .gitignore                           # Ignores Library, Temp, etc.
├── README.md                            # Repository README (installation guide)
├── SETUP_GITHUB.md                      # GitHub setup instructions
├── CLAUDE.md                            # Project context for Claude Code
├── PACKAGE_STRUCTURE.md                 # This file
│
├── Assets/
│   ├── UFetch/                          # ← THE PACKAGE (installed by users)
│   │   ├── Runtime/
│   │   │   ├── UFetch.cs
│   │   │   └── UFetch.Runtime.asmdef
│   │   ├── Samples~/
│   │   │   └── Demo/
│   │   │       └── Demo.cs
│   │   ├── package.json
│   │   ├── README.md
│   │   ├── LICENSE.md
│   │   └── UFetch_Documentation.md
│   │
│   ├── Scripts/                         # Demo scripts (not part of package)
│   │   └── Demo.cs
│   ├── Scenes/                          # Demo scene (not part of package)
│   │   └── SampleScene.unity
│   └── Settings/                        # URP settings (not part of package)
│
├── Packages/
│   ├── manifest.json                    # Project dependencies
│   └── packages-lock.json
│
├── ProjectSettings/                     # Unity project settings
│
└── Library/                             # Unity cache (ignored by git)
```

## 🔑 Important Notes

### What Gets Installed
When users install UFetch via Package Manager, **only** the contents of `Assets/UFetch/` are installed to their `Packages/` folder, specifically:
- Runtime code
- Documentation
- License
- package.json
- Samples (available but not imported by default)

### What Doesn't Get Installed
- The demo scene (`Assets/Scenes/`)
- The demo scripts (`Assets/Scripts/`)
- URP settings (`Assets/Settings/`)
- Unity project files
- Repository docs (`SETUP_GITHUB.md`, `CLAUDE.md`, etc.)

### Why This Structure?
This structure allows you to:
1. **Develop and test** UFetch within a full Unity project
2. **Publish only the package** using the `?path=/Assets/UFetch` parameter
3. **Keep examples** in the main project without forcing them on users
4. **Maintain documentation** in both the repository and the package

## 🔄 Update Workflow

When making changes:

1. **Update code** in `Assets/UFetch/Runtime/`
2. **Update version** in `Assets/UFetch/package.json`
3. **Update docs** if API changed
4. **Test** in Unity Editor
5. **Commit and push** to GitHub
6. **Tag the release**: `git tag v1.0.1 && git push origin v1.0.1`
7. Users can now install the new version

## ✅ Verification Checklist

Before pushing to GitHub, verify:

- [ ] `Assets/UFetch/package.json` exists with correct name and version
- [ ] `Assets/UFetch/Runtime/UFetch.cs` exists
- [ ] `Assets/UFetch/Runtime/UFetch.Runtime.asmdef` exists
- [ ] `Assets/UFetch/README.md` has installation instructions
- [ ] `Assets/UFetch/LICENSE.md` exists
- [ ] `.gitignore` ignores Library, Temp, etc.
- [ ] Root `README.md` has installation instructions
- [ ] All file paths use forward slashes or are properly escaped

## 📦 Package Installation Path

After installation, users will find UFetch at:
```
Packages/com.yourname.ufetch/
├── Runtime/
│   ├── UFetch.cs
│   └── UFetch.Runtime.asmdef
├── Samples~/
├── package.json
├── README.md
├── LICENSE.md
└── UFetch_Documentation.md
```

## 🎯 Best Practices

1. **Keep Runtime/ clean** - Only production code
2. **Use Samples~/** - For example code (~ prevents Unity from processing it)
3. **Version correctly** - Follow semantic versioning (major.minor.patch)
4. **Document changes** - Update docs with each version
5. **Test before push** - Always test in a clean project
6. **Use tags** - Create Git tags for each version
7. **Keep package.json updated** - Increment version with each change

## 🔗 Useful Commands

```bash
# View package structure
find Assets/UFetch -type f -not -name "*.meta"

# Check package.json validity
cat Assets/UFetch/package.json | python -m json.tool

# Create a release tag
git tag v1.0.0
git push origin v1.0.0

# Test installation URL (replace with your username)
echo "https://github.com/yourusername/UFetch.git?path=/Assets/UFetch"
```

## 📚 References

- [Unity Package Manager Documentation](https://docs.unity3d.com/Manual/Packages.html)
- [Custom Package Layout](https://docs.unity3d.com/Manual/cus-layout.html)
- [Git URLs in Package Manager](https://docs.unity3d.com/Manual/upm-git.html)
- [Semantic Versioning](https://semver.org/)

---

**Ready to push to GitHub?** Follow the instructions in [SETUP_GITHUB.md](./SETUP_GITHUB.md)
