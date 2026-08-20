#!/bin/bash

NC='\033[0m'
YELLOW='\033[0;33m'
RED='\033[0;31m'
MAGENTA='\033[0;35m'
CYAN='\033[0;36m'

if [[ -z $1 ]]; then
	echo -e "${RED}The first argument is missing and must be a semver number!${NC}"
	exit
fi
version="$1"

if [[ -n $2 ]]; then
	prerelease="$2"
fi

if [[ -n $prerelease ]]; then
	final="${version}-${prerelease}"
else
	final=$version
fi
if [ $? -ne 0 ]; then
	echo -e "${RED}Error when setting version!${NC}"
	return
fi
echo -e "Setting version to ${YELLOW}${final}${NC}."

destination="packages/${final}"

echo -e "\nPacking ${MAGENTA}AppInfo${NC}."
dotnet pack "src/AppInfo/AppInfo.csproj" \
	--nologo --property WarningLevel=0 --force \
	--include-symbols \
	--runtime win-x64 \
	--configuration Release \
	--output "$destination" \
	-p:Version="${final}" \
	-p:TargetFrameworks="net8.0"
if [ $? -ne 0 ]; then
	echo -e "${RED}Error when packing ${MAGENTA}AppInfo${RED}!${NC}"
	return
fi

echo -e "\nSuccessfully packaged version ${YELLOW}${final}${NC} to folder ${CYAN}${destination}${NC}."
