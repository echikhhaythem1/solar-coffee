# Project Variables
 PROJECT_NAME ?= SolarCoffee
 ORG_Name ?= SolarCoffee
 REPO_Name ?= SolarCoffee

.PHONY: migrations db
	migrations:

		cd ./SolarCoffee.Data && add-migration ${mname} && cd ..

db:             cd ./SolarCoffee.Data && update-database && cd .. 
