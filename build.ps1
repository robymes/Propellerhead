docker rmi robymes-propellerhead-build
docker rmi robymes-propellerhead
docker build --target=build-stage -t robymes-propellerhead-build .
docker build --target=final-stage -t robymes-propellerhead .
docker rmi robymes-propellerhead-build