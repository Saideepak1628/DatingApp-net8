import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../member-edit/member-edit.component';


export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  if (component.editForm?.dirty)
  {
    return confirm('ARe you sure you wnat to continue ? any unsaved changes will be lost')
  }
  return true;
};
