package de.mkoertgen.samples.signalr;

import java.util.Scanner;

import microsoft.aspnet.signalr.client.Action;
import microsoft.aspnet.signalr.client.ErrorCallback;
import microsoft.aspnet.signalr.client.LogLevel;
import microsoft.aspnet.signalr.client.Logger;
import microsoft.aspnet.signalr.client.MessageReceivedHandler;
import microsoft.aspnet.signalr.client.hubs.HubConnection;
import microsoft.aspnet.signalr.client.hubs.HubProxy;

import com.google.gson.JsonElement;

public class Program {

  public static void main(String[] args) {
    
    Logger logger = new Logger() {
      @Override
      public void log(String message, LogLevel level) {
        //System.out.println(message);
      }
    };

    String url = "http://localhost:8080";
    if (args.length > 0) {
      url = args[0];
    }

    // Connect to the server
    HubConnection conn = new HubConnection(url, "", true, logger);
    
    // Create the hub proxy
    HubProxy proxy = conn.createHubProxy("ChatHub");

    proxy.subscribe(new Object() {
      @SuppressWarnings("unused")
      public void broadcastMessage(String name, String message) {
        System.out.format("[%s]: '%s'\n", name, message);
      }
    });

    // Subscribe to the error event
    conn.error(new ErrorCallback() {
      @Override
      public void onError(Throwable error) {
        error.printStackTrace();
      }
    });

    conn.connected(new Runnable() {
      @Override
      public void run() {
        System.out.println("CONNECTED");
      }
    });

    // Subscribe to the closed event
    conn.closed(new Runnable() {
      @Override
      public void run() {
        System.out.println("DISCONNECTED");
      }
    });

    // Start the connection
    conn.start()
      .done(new Action<Void>() {
        @Override
        public void run(Void obj) throws Exception {
          System.out.println("Done Connecting!");
        }
      });

    /*    
    // Subscribe to the received event
    conn.received(new MessageReceivedHandler() {
      @Override
      public void onMessageReceived(JsonElement json) {
        System.out.println("RAW received message: " + json.toString());
      }
    });
    */

    System.out.println("Start typing to chat. 'exit' to quit.");

    Scanner inputReader = new Scanner(System.in);
    String line = inputReader.nextLine();
    while (!"exit".equals(line)) {
      proxy.invoke("send", "Console:java", line).done(new Action<Void>() {
        @Override
        public void run(Void obj) throws Exception {
          System.out.println("SENT!");
        }
      });

      line = inputReader.next();
    }

    inputReader.close();

    conn.stop();
  }
}