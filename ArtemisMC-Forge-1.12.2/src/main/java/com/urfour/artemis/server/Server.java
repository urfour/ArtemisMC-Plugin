package com.urfour.artemis.server;

import com.google.gson.Gson;
import com.urfour.artemis.infos.MinecraftInfos;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClientBuilder;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.nio.file.Files;
import java.nio.file.Paths;

public class Server implements Runnable {
    private static String getArtemisFolder() {
        if (System.getProperty("os.name").contains("Windows")) {
            return System.getenv("ProgramData");
        }
        else {
            return System.getProperty("user.home") + "/.local/share";
        }
    }
    private MinecraftInfos infos = new MinecraftInfos();
    private static final Logger LOGGER = LogManager.getLogger("artemis-server");
    private static final String WEB_SERVER_FILE = getArtemisFolder() + "/Artemis/webserver.txt";
    private String IP;
    private Gson gson = new Gson();
    public Server() {
        try {
            IP = Files.readAllLines(Paths.get(WEB_SERVER_FILE)).get(0);
            LOGGER.info("Using IP " + IP + " to send in-game information.");
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
    public void run() {
        try {
            infos.update();
            //LOGGER.info(gson.toJson(infos));
            CloseableHttpClient httpClient = HttpClientBuilder.create().build();
            HttpPost request = new HttpPost(IP + "plugins/25dacd2d-9275-4d94-bc12-8761dedf0f1d/Minecraft");
            request.addHeader("Content-Type", "application/json");
            request.setEntity(new StringEntity(gson.toJson(infos)));
            httpClient.execute(request);
        } catch (Exception ex) {
            LOGGER.error(ex);
        }
    }

}
