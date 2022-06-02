package com.intel.eurekasever;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.server.EnableEurekaServer;

@SpringBootApplication
@EnableEurekaServer
public class EurekaseverApplication {

	public static void main(String[] args) {
		SpringApplication.run(EurekaseverApplication.class, args);
	}

}
