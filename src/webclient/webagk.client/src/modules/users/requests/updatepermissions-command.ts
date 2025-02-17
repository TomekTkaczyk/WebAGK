export default interface IUpdatePermissionsCommand {
  id: string,
  role: string,
  isActive: boolean,
  permissions: {},
}
