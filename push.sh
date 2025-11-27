#!/bin/bash

MSG=${1:-"sync codebase"}

git add -A

# push
git commit -m "$MSG" && git push