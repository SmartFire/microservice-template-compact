require 'physique'

# Reports version information about the build to TeamCity. If you are not 
# using TeamCity, you should delete this line.
require 'albacore/ext/teamcity' 

Physique::Solution.new do |s|
  s.file = 'src/__NAME__.sln'

  s.run_tests do |t|
    # Find all assemblies ending in 'Tests' excluding 'AcceptanceTests'.
    t.files = FileList["**/*Tests/bin/Release/*Tests.dll"]
  end

  # Sets up the FluentMigrator workflow tasks for your database project.
  s.database do |db|
    db.instance = ENV['DATABASE_SERVER'] || 'localhost'
    db.name = ENV['DATABASE_NAME'] || '__NAME__'
    db.project = '__NAME__.Database'
  end

  # Publish the Api, MessageBus, and Database projects to Octopus Deploy.  If 
  # you are not using Octopus Deploy you should delete this section.
  s.octopus_deploy do |octo|
    octo.server = 'http://build/nuget/packages'
    octo.api_key = ENV['OCTOPUS_API_KEY']

    octo.deploy_app do |app|
      app.name = 'database'
      app.type = :console
      app.project = '__NAME__.Database'

      app.with_metadata do |m|
        m.description = '__NAME__ Database Migrations'
        m.authors = 'Third Wave Technology'
      end
    end

    octo.deploy_app do |app|
      app.name = 'service'
      app.type = :console
      app.project = '__NAME__.MessageBus'

      app.with_metadata do |m|
        m.description = '__NAME__ Message Bus'
        m.authors = 'Third Wave Technology'
      end
    end
  end
end

desc 'Runs the continuous integration build'
task :ci => [:versionizer, :test] # Add any steps you want included in your CI 
                                  # build here.

desc 'Runs the acceptance tests'
test_runner :acceptance do |t|
  t.files = FileList["**/*.AcceptanceTests/bin/Release/*.Tests.Acceptance.dll"]
  t.exe = 'src/packages/NUnit.Runners.2.6.3/tools/nunit-console.exe'
  %w(/labels /trace=Verbose).each { |p| t.parameters.add p }
end

self.extend Physique::DSL