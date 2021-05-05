# Data aggregation with Ocelot

This is a demo to try out ocelot and data aggregation with api gateway

#### Getting Started
1. Nuget: Install-Package Ocelot.
2. Configuration: create ocelot.json and define the ReRoutes.
3. Program: configBuilder.AddJsonFile("ocelot.json").
4. Startup: services.AddOcelot.
5. Startup: app.UseOcelot().
6. Run.

#### Resources

- [Ocelot](https://ocelot.readthedocs.io) home page, documentation.
- [Ocelot - GitHub page](https://github.com/ThreeMammals/Ocelot).
