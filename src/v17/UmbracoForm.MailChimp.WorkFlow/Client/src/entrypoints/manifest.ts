export const manifests: Array<UmbExtensionManifest> = [
  {
    name: "Umbraco Form Mail Chimp Work Flow Entrypoint",
    alias: "UmbracoForm.MailChimp.WorkFlow.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint.js"),
  },
];
