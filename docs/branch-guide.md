# BRANCH GUIDE

Following are the most important branches:

- `develop`: Contains the latest code **and it is the branch actively developed**. Note that **all PRs must be against `develop` branch to be considered**. 

- `main`: Synced time to time from develop. It contains "stable" code, although not the latest one. The plan to do periodic merges from `develop` to `main`, but not right now.

Any other branch is considered temporary and could be deleted at any time. Do not do any PR to them!
To make new features, you branch off of `develop` into feature branches i.e. `feature/my-new-feature`. To prep a new release for deployment, you make a release branch off of develop i.e. `release/1.4.9`, which you then merge to main. 

To do a hotfix on production/main, you make a hotfix branch off of main, i.e. `hotfix/fix-prod-bug`, which you merge back into main.

Thanks!
