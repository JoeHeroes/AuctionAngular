import { Component } from "@angular/core";
import { Employee, Service, State } from "./sr.serivce";

@Component({
  selector: 'app-sevices',
  templateUrl: './sevices.component.html',
  styleUrls: ['./sevices.component.css']
})
export class SevicesComponent {
  employees: Employee[];

  states: State[];

  constructor(private service: Service) {
    this.employees = service.getEmployees();
    this.states = service.getStates();
    this.cloneIconClick = this.cloneIconClick.bind(this);
  }

  private static isChief(position: any) {
    return position && ['CEO', 'CMO'].indexOf(position.trim().toUpperCase()) >= 0;
  }

  rowValidating(e :any) {
    const position = e.newData.Position;

    if (SevicesComponent.isChief(position)) {
      e.errorText = `The company can have only one ${position.toUpperCase()}. Please choose another position.`;
      e.isValid = false;
    }
  }

  editorPreparing(e :any) {
    if (e.parentType === 'dataRow' && e.dataField === 'Position') {
      e.editorOptions.readOnly = SevicesComponent.isChief(e.value);
    }
  }

  allowDeleting(e :any) {
    return !SevicesComponent.isChief(e.row.data.Position);
  }

  isCloneIconVisible(e :any) {
    return !e.row.isEditing;
  }

  isCloneIconDisabled(e :any) {
    return SevicesComponent.isChief(e.row.data.Position);
  }

  cloneIconClick(e :any) {
    const clonedItem = { ...e.row.data, ID: this.service.getMaxID() };

    this.employees.splice(e.row.rowIndex, 0, clonedItem);
    e.event.preventDefault();
  }


  
}



