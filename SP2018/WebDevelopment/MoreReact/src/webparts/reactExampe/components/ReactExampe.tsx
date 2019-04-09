import * as React from 'react';
import styles from './ReactExampe.module.scss';
import { escape } from '@microsoft/sp-lodash-subset';
import { sp, Web } from "@pnp/sp";
import { Spinner } from 'office-ui-fabric-react/lib/Spinner';

export interface IReactExampeProps {
  description: string;
  color: string;
}

export interface IReactExampeState {
  propertyPageValues: any;
  loading: boolean;
}


export default class ReactExampe extends React.Component<IReactExampeProps, IReactExampeState> {

  constructor(props: IReactExampeProps) {
    super(props);
    this.state = {
      propertyPageValues: {
        vti_x005f_defaultlanguage: "empty"
      },
      loading: true
    };
  }

  public async componentDidMount() {


// let props = await sp.web.allProperties.select("ProgramId").get();

// let programId = props.ProgramId;

// let studentportalWeb = new Web("https://folkis2018.sharepoint.com/sites/Hamada10/"); // student portal

// let program = await studentportalWeb.lists.getByTitle("Programs").items.getById(programId).get();
// let courses = await studentportalWeb.lists.getByTitle("Courses").items.filter("ProgramId eq " + programId).get();



    sp.web.allProperties.get().then(data => {
      console.log(data);
      this.setState({ propertyPageValues: data, loading : false });
    });



  }



  public render(): React.ReactElement<IReactExampeProps> {
    return (
      <div>
        {this.state.loading &&
          <div>
            <Spinner label="Seriously, still loading..." ariaLive="assertive" labelPosition="top" />
          </div>
        }
        {!this.state.loading &&
          <div>
            {this.state.propertyPageValues.vti_x005f_defaultlanguage}
          </div>
        }
      </div>
    );
  }
}
