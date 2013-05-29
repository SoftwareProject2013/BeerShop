<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="BeerShop.Azure1" generation="1" functional="0" release="0" Id="850f8130-4f75-40f8-ae9d-bfdc3c9d2f67" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="BeerShop.Azure1Group" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="BeerShop:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/LB:BeerShop:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="BeerShop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/MapBeerShop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="BeerShopInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/MapBeerShopInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:BeerShop:Endpoint1">
          <toPorts>
            <inPortMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShop/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapBeerShop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShop/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBeerShopInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShopInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="BeerShop" generation="1" functional="0" release="0" software="D:\Beer\BeerShop\BeerShop.Azure1\csx\Debug\roles\BeerShop" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BeerShop&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BeerShop&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShopInstances" />
            <sCSPolicyUpdateDomainMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShopUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShopFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="BeerShopUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="BeerShopFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="BeerShopInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="e8a5f3dd-ad19-4ca7-a921-12772d653735" ref="Microsoft.RedDog.Contract\ServiceContract\BeerShop.Azure1Contract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="ef4417d5-4c22-4127-990d-8a2aab395d26" ref="Microsoft.RedDog.Contract\Interface\BeerShop:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShop:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>