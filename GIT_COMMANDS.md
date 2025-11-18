# Quick Git Commands for UFetch

This file contains ready-to-use Git commands for pushing UFetch to GitHub.

## 📝 Before You Start

1. **Update package.json**:
   - Open `Assets/UFetch/package.json`
   - Change `"com.yourname.ufetch"` to your actual package name
   - Update `"name"` and `"url"` in the author section

2. **Update README.md**:
   - Replace `yourusername` with your GitHub username in all URLs
   - Update any personal information

## 🚀 Initial Setup (First Time Only)

Open a terminal/command prompt in the project root (`d:\Unity\UFetch`) and run these commands:

```bash
# Initialize git repository
git init

# Add all files to staging
git add .

# Create initial commit
git commit -m "Initial commit: UFetch Unity Package"
```

## 🌐 Create GitHub Repository

1. Go to https://github.com/new
2. Repository name: `UFetch`
3. Description: `Modern async/await HTTP client for Unity`
4. Public or Private: Choose based on your needs
5. **DO NOT** check "Initialize with README"
6. Click **Create repository**

## 🔗 Connect to GitHub

After creating the repository, run these commands (replace `yourusername` with your actual GitHub username):

```bash
# Add GitHub as remote
git remote add origin https://github.com/yourusername/UFetch.git

# Rename branch to main (if needed)
git branch -M main

# Push to GitHub
git push -u origin main
```

## 🏷️ Create First Release

```bash
# Create and push version tag
git tag v1.0.0
git push origin v1.0.0
```

Then go to your GitHub repository and create a release:
1. Click **Releases** → **Create a new release**
2. Choose tag: `v1.0.0`
3. Release title: `UFetch v1.0.0 - Initial Release`
4. Add description of features
5. Click **Publish release**

## 🔄 Making Updates

When you make changes to UFetch:

```bash
# 1. Update version in Assets/UFetch/package.json (e.g., "1.0.1")

# 2. Stage and commit changes
git add .
git commit -m "Update to v1.0.1: [describe changes]"

# 3. Push changes
git push

# 4. Create new version tag
git tag v1.0.1
git push origin v1.0.1

# 5. Create GitHub release (optional but recommended)
```

## 📦 Installation URL

After pushing to GitHub, share this URL with users:

```
https://github.com/yourusername/UFetch.git?path=/Assets/UFetch
```

Or for a specific version:

```
https://github.com/yourusername/UFetch.git?path=/Assets/UFetch#v1.0.0
```

## 🔍 Verify Your Setup

After pushing, verify everything works:

```bash
# Check remote URL
git remote -v

# Check current branch
git branch

# Check recent commits
git log --oneline -5

# Check tags
git tag -l
```

## ⚠️ Common Issues

### "fatal: 'origin' already exists"
```bash
# Remove and re-add origin
git remote remove origin
git remote add origin https://github.com/yourusername/UFetch.git
```

### "Permission denied" or authentication issues
```bash
# Use Personal Access Token instead of password
# 1. Go to GitHub Settings → Developer settings → Personal access tokens
# 2. Generate new token with 'repo' permissions
# 3. Use token as password when prompted
```

### Forgot to update package.json before pushing
```bash
# 1. Update package.json with correct info
# 2. Commit and push
git add Assets/UFetch/package.json
git commit -m "Update package.json with correct author info"
git push
```

## 📋 Complete Command Sequence (Copy-Paste)

Replace `yourusername` with your GitHub username, then copy and paste:

```bash
# Initialize (if not already done)
git init
git add .
git commit -m "Initial commit: UFetch Unity Package"

# Connect to GitHub (replace yourusername!)
git remote add origin https://github.com/yourusername/UFetch.git
git branch -M main
git push -u origin main

# Create first release tag
git tag v1.0.0
git push origin v1.0.0

# Done! Your package is now on GitHub
```

## ✅ Post-Push Checklist

After pushing, verify:

- [ ] Repository is visible on GitHub
- [ ] All files are uploaded (check Assets/UFetch/ folder)
- [ ] Release tag exists (v1.0.0)
- [ ] Installation URL works: `https://github.com/yourusername/UFetch.git?path=/Assets/UFetch`
- [ ] Test installation in a new Unity project

## 🧪 Test Installation

To test your package:

1. Create a new Unity project
2. Open Package Manager
3. Click + → Add package from git URL
4. Enter your Git URL with ?path parameter
5. Verify UFetch appears in Packages/
6. Test the code in a MonoBehaviour

## 📞 Need Help?

- Check [SETUP_GITHUB.md](./SETUP_GITHUB.md) for detailed instructions
- See [PACKAGE_STRUCTURE.md](./PACKAGE_STRUCTURE.md) for structure details
- GitHub docs: https://docs.github.com/en/get-started
- Unity UPM docs: https://docs.unity3d.com/Manual/upm-git.html

---

**Ready to go?** Run the commands above and you're done! 🎉
