#!/bin/bash
# Script to install Npgsql package to all JobMatix projects
# This will use NuGet to add Npgsql to .NET Framework projects

echo "Installing Npgsql NuGet package to all JobMatix projects..."
echo "============================================================"
echo ""

# Define all project directories
projects=(
    "JMxJT620.NET/JMxJT620.vbproj"
    "JMxPOS620.Net/JMxPOS620.vbproj"
    "JobMatix62.Net/JobMatix62.vbproj"
    "JMxRAs62.Net/JobMatixRAs62.vbproj"
    "JMxRetailHost620.Net/JMxRetailHost620.vbproj"
    "JMxJT620ex.Net/JMxJT620ex.vbproj"
    "backup-agent/JMxBackupAgent-Build-6201/JMxBackupAgent.net/JMxBackupAgent.vbproj"
)

# Check if nuget is available
if ! command -v nuget &> /dev/null; then
    echo "NuGet not found. Downloading nuget.exe..."
    wget -q https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -O nuget.exe
    if [ $? -ne 0 ]; then
        echo "❌ Failed to download NuGet. Please install NuGet manually."
        echo "   Visit: https://www.nuget.org/downloads"
        exit 1
    fi
    NUGET_CMD="mono nuget.exe"
else
    NUGET_CMD="nuget"
fi

# Install Npgsql for each project
for project in "${projects[@]}"; do
    if [ -f "$project" ]; then
        echo "📦 Installing Npgsql to: $project"
        project_dir=$(dirname "$project")
        
        # For .NET Framework 3.5, we need Npgsql 3.x (latest that supports it)
        # Npgsql 3.2.7 is the last version supporting .NET 3.5
        cd "$project_dir" || continue
        $NUGET_CMD install Npgsql -Version 3.2.7 -OutputDirectory packages
        cd - > /dev/null
        
        echo "   ✅ Npgsql installed to $project_dir/packages"
    else
        echo "   ⚠️  Project not found: $project"
    fi
    echo ""
done

echo "============================================================"
echo "✅ Npgsql installation complete!"
echo ""
echo "Note: You'll need to add the package reference to each .vbproj file:"
echo "  <Reference Include=\"Npgsql\">"
echo "    <HintPath>packages\\Npgsql.3.2.7\\lib\\net35\\Npgsql.dll</HintPath>"
echo "  </Reference>"
echo ""
echo "Or use Visual Studio's Manage NuGet Packages UI to add the reference."
