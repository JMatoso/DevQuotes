# DevQuotes

See the documentation here: [API Documentation](https://codequotes.herokuapp.com/swagger/)

<h3>Tools</h3>
<ul>
    <li>VS 2022</li>
    <li>.NET Core 6.0</li>
</ul>

<h3>How to run</h3>
<p>Restore all packages:</p>

```
dotnet restore
```

<p>Run:</p>

```
dotnet run
```

<p>Deploy:</p>

```
heroku login
heroku git:remote -a <project-name>
heroku buildpacks:set https://github.com/jincod/dotnetcore-buildpack

git push heroku master
```
