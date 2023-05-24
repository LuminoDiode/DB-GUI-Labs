docker build . -t "luminodiode/dbgui_client:nightly" -t "luminodiode/dbgui_client:latest";
docker push "luminodiode/dbgui_client:0.3.1-beta"; docker push "luminodiode/dbgui_client:latest";
pause "Press any key to exit";