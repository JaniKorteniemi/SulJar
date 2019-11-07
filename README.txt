https://learngitbranching.js.org/

git config --global user.name "name"

git config --global usser.email ur@email.com

git config --list

//help
git help
git <verb> --help

//new repository
git init

//file status
git status

//Track new file
git add <filename>

//commit staged file
git commit

//ignoring file
/create .gitignore file and list the files/folders

//forgot to add file
git commit --amend

//Staged wrong file and want to unstageit
git rest HEAD <ur file>

//Stop tracking a file but keep local copy
git rm --cachehd <filename>

//New Branch
git branch <BranchName>

//pick branch
git checkout <BranchName>

//Merge Branches
git merge <BranchName>

//Plot commit copies in Branch tree
git rebase <BranchName>

//see hashes
git log

//Moving upwards one commit at a time with ^
//Moving upwards a number of times with ~<num>

//will move a branch backwards as if the commit had never been made in the first place.
git reset <BranchName>