# Library Management - LinQ

## Description
Ce projet est une application de gestion de bibliothèque développée en C#. Il permet de gérer une collection de livres, d'auteurs, d'emprunteurs et d'emprunts. L'application utilise LINQ pour effectuer des requêtes complexes sur les données.

## Fonctionnalités

### Gestion des Livres
- Affichage de tous les livres
- Recherche de livres par titre, auteur ou catégorie
- Affichage des livres disponibles
- Affichage des livres empruntés
- Affichage des livres en retard
- Affichage des livres par auteur
- Affichage des livres par catégorie

### Gestion des Emprunteurs
- Affichage des emprunteurs actifs
- Gestion des emprunts
- Suivi des retards

### Export de Données
- Export des données en format XML
- Export des données en format JSON
- Possibilité d'exporter les résultats de recherche et d'affichage

## Structure du Projet

### Modèles
- `Library.cs` : Classe principale gérant la bibliothèque
- `Book.cs` : Modèle représentant un livre
- `Author.cs` : Modèle représentant un auteur
- `Borrower.cs` : Modèle représentant un emprunteur
- `Loan.cs` : Modèle représentant un emprunt

### Organisation des Fichiers
- `DataSource/` : Contient le fichier XML source des données
- `Exports/` : Dossier où sont stockés les fichiers d'export
- `Models/` : Contient les classes modèles

## Utilisation

### Initialisation
L'application charge automatiquement les données depuis le fichier XML situé dans le dossier `DataSource` au lancement du programme.

### Menu Principal
1. Afficher tous les livres
2. Rechercher un livre
3. Afficher les livres disponibles
4. Afficher les livres empruntés
5. Afficher les livres en retard
6. Afficher les livres par auteur
7. Afficher les livres par catégorie
8. Afficher les emprunteurs
Q. Quitter

### Export de Données
Pour chaque affichage de données, vous avez la possibilité d'exporter les résultats en :
1. Format XML
2. Format JSON

Ou bien de retourner au menu principal avec le troisième sous-menu.

Vous serez ensuite inviter à sélectionner les champs que vous désirez exporter.

Les fichiers extraits sont localisés dans le répertoire `bin > Debug > net8.0 > Exports`.

## Technologies Utilisées
- C#
- LINQ (Language Integrated Query)
- XML
- JSON

## Prérequis
- .NET 8.0 ou supérieur
- Visual Studio 2022 (recommandé)

## Installation
1. Cloner le dépôt
2. Ouvrir la solution dans Visual Studio
3. Compiler et exécuter le projet

**Attention** : Une étape de nettoyage de la solution pourrait être nécessaire si la solution ne fonctionne pas comme prévu.

## Structure des Données

### Livre (Book)
- ID
- Titre
- Auteur
- Date de publication
- ISBN
- Catégorie
- Nombre de pages

### Auteur (Author)
- ID
- Nom
- Nationalité
- Date de naissance

### Emprunteur (Borrower)
- ID
- Nom
- Email
- Téléphone

### Emprunt (Loan)
- ID
- Livre
- Emprunteur
- Date d'emprunt
- Date de retour prévue
- Date de retour effective