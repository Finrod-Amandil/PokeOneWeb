<div id="top"></div>

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=poke-one-web-frontend&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=poke-one-web-frontend)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=poke-one-web-api&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=poke-one-web-api)
![release staging environment workflow](https://github.com/Finrod-Amandil/PokeOneWeb/actions/workflows/release-stage.yaml/badge.svg)
![build pokeone webapp](https://github.com/Finrod-Amandil/PokeOneWeb/actions/workflows/pokeone-webapp.yaml/badge.svg)
![build pokeone api](https://github.com/Finrod-Amandil/PokeOneWeb/actions/workflows/pokeone-api.yaml/badge.svg)



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Finrod-Amandil/PokeOneWeb">
    <img src="Documentation/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Unofficial PokéOne Guide</h3>

  <p align="center">
    The PokéOne guide gives you all the information you need for a successful gameplay.
    <br />
    <a href="https://github.com/Finrod-Amandil/PokeOneWeb/wiki"><strong>Explore the docs »</strong></a>
    <br />
    <br />
  </p>
</div>

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow the steps in the [Developer Installation Guide](https://docs.google.com/document/d/1oLbs6IRg8wib5WZGg_G80hXRQTvkAxb8WLfT5Gxu71U).

## Deployment of the application
Continuous integration and deployment of the application is done using GitHub Actions.

On every push, the project will be built and test will be run inside the build pipeline. The outcome can be shown in the Github Actions tab on [github.com](https://github.com/Finrod-Amandil/PokeOneWeb/actions).

On pushes to the `development` branch (eg. via Pull-Request), the application will be compiled and packaged.

After that, the package is uploaded to the hosting machine and deployed to the development environment.

## Built With

The application is built with the following libraries and frameworks.

* [Angular](https://angular.io/)
* [DotNet](https://dotnet.microsoft.com/en-us/)


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Finrod-Amandil/PokeOneWeb.svg
[contributors-url]: https://github.com/Finrod-Amandil/PokeOneWeb/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Finrod-Amandil/PokeOneWeb.svg
[forks-url]: https://github.com/Finrod-Amandil/PokeOneWeb/network/members
[stars-shield]: https://img.shields.io/github/stars/Finrod-Amandil/PokeOneWeb.svg
[stars-url]: https://github.com/Finrod-Amandil/PokeOneWeb/stargazers